using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;
using ReadyPlayerMe.Core;

namespace ReadyPlayerMe.Loadtest
{
    public class AvatarLoadingHandler : MonoBehaviour
    {
        private const string AVATAR_ID_CSV = "avatar_ids.csv";
        private const string BASE_URL = "https://models.readyplayer.me/";
        [SerializeField] private GameObject loadingPlaceholder;
        [Range(1,10), SerializeField] private int maxAvatarLoaders = 1;
        [SerializeField] private RuntimeAnimatorController animatorController;
        private readonly List<Avatar> avatars = new List<Avatar>();
        private float loadingTime = 0;
        private bool loading;
        private GameObject placeholderAvatar;
        private AvatarIDReader avatarIDReader;
        private Dictionary<string, GameObject> placeholderAvatarMap = new Dictionary<string, GameObject>();
        private AvatarObjectLoader avatarLoader;
        private float loadingStartTime;
        private int avatarBatchToLoad = 1;
        public event EventHandler<AvatarLoadedEventArgs> AvatarLoaded;
        public event EventHandler<AllAvatarsLoadedEventArgs> AllAvatarsLoaded;
        
        private int numberOfFailedDownloads = 0;
        private float folderSizeBeforeLoad = 0;
        private AvatarConfig avatarConfig;

        private void Start()
        {          
            avatarIDReader = new AvatarIDReader();
            avatarIDReader.ReadCSVFromResources(AVATAR_ID_CSV);
        }

        private void Update()
        {
            loadingTime += Time.deltaTime;
        }

        public void LoadAvatars(int numberOfAvatarsToLoad, AvatarConfig config)
        {
            avatarConfig = config;
            StartCoroutine(LoadAvatars(numberOfAvatarsToLoad, config, avatarIDReader.AvatarList));
        }

        private void OnAvatarLoaded(AvatarLoadedEventArgs e)
        {
            EventHandler<AvatarLoadedEventArgs> handler = AvatarLoaded;
            handler?.Invoke(this, e);
        }

        private void OnAllAvatarsLoaded(AllAvatarsLoadedEventArgs e)
        {
            EventHandler<AllAvatarsLoadedEventArgs> handler = AllAvatarsLoaded;
            handler?.Invoke(this, e);
        }

        private IEnumerator LoadAvatars(int numberOfAvatarsToLoad, AvatarConfig config, List<string> avatarList)
        {
            numberOfFailedDownloads = 0;
            loadingStartTime = Time.time;
            avatarBatchToLoad = numberOfAvatarsToLoad;
            var semaphore = new SemaphoreSlim(maxAvatarLoaders);

            List<Task> downloadTasks = new List<Task>();
            
            for (var i = 0; i < numberOfAvatarsToLoad && i < avatarList.Count; i++)
            {
                yield return semaphore.WaitAsync();

                var avatarID = avatarList[i];
                downloadTasks.Add(DownloadAvatar(avatarID, config, semaphore));
            }

            // Wait for all tasks to complete
            yield return new WaitUntil(() => Task.WhenAll(downloadTasks).IsCompleted);
            float totalSize = 0;
            foreach (var avatar in avatars)
            {
                totalSize += AvatarCache.GetAvatarDataSizeInMb(avatar.AvatarID);
            }
            var fileDownloadSize = GetTotalAvatarSize();
            OnAllAvatarsLoaded(new AllAvatarsLoadedEventArgs(CalcSumLoadingTime(), fileDownloadSize, CalcAverageDownloadSize(fileDownloadSize)));
            Debug.Log($"All avatars loaded in {CalcSumLoadingTime()} seconds. Failed downloads: {numberOfFailedDownloads} / {avatarBatchToLoad}");
            
        }
        
        private async Task DownloadAvatar(string avatarID, AvatarConfig avatarConfig, SemaphoreSlim semaphore)
        {
            loading = true;
            loadingTime = 0;
            
            avatarLoader = new AvatarObjectLoader();
            avatarLoader.AvatarConfig = avatarConfig;

            var avatarUrl = GetAvatarUrl(avatarID);
            InstantiateLoadingPlaceholder(avatarUrl);
            
            avatarLoader.OnCompleted += OnLoadingCompleted;
            avatarLoader.OnFailed += OnLoadingFailed;

            await avatarLoader.LoadAvatarAsync(avatarUrl);

            // Release the semaphore once the download is complete
            semaphore.Release();
        }

        private Vector3 InstantiateLoadingPlaceholder(string url)
        {
            var pos = Quaternion.Euler(90, 0, 0) * Random.insideUnitCircle * 15; //random position in a circle with r=15
            placeholderAvatar = Instantiate(loadingPlaceholder);
            placeholderAvatarMap.Add(url, placeholderAvatar);
            return placeholderAvatar.transform.position = pos;
        }

        private void OnLoadingCompleted(object sender, CompletionEventArgs args)
        {
            Debug.Log("Loaded: " + args.Metadata.BodyType);
            args.Avatar.transform.SetParent(gameObject.transform);
            if (placeholderAvatarMap.TryGetValue(args.Url, out var placeholderObject))
            {
                args.Avatar.transform.position = placeholderObject.transform.position;
                DestroyImmediate(placeholderObject);
                placeholderAvatarMap.Remove(args.Url);
            };
            
            AvatarAnimationHelper.SetupAnimator(args.Metadata, args.Avatar);
            var animator = args.Avatar.GetComponent<Animator>();
            if (animator != null)
            {
                animator.runtimeAnimatorController = animatorController;
            }
            var avatar = args.Avatar.AddComponent<Avatar>();
            avatar.AvatarDownloaded(args.Metadata, loadingTime, avatar.name);
            avatars.Add(avatar);
            var fileDownloadSize = GetTotalAvatarSize();
            OnAvatarLoaded(new AvatarLoadedEventArgs(
                avatar,
                CalcAverageLoadingTime(), 
                CalcSumLoadingTime(),
                CalcAverageDownloadSize(fileDownloadSize), 
                fileDownloadSize));
            
            loading = false;
        }

        private string GetAvatarUrl(string avatarID)
        {
            return $"{BASE_URL}{avatarID}.glb";
        }

        private void OnLoadingFailed(object sender, FailureEventArgs e)
        {
            numberOfFailedDownloads++;
            if (placeholderAvatarMap.TryGetValue(e.Url, out var placeholderObject))
            {
                DestroyImmediate(placeholderObject);
                placeholderAvatarMap.Remove(e.Url);
            };
            Debug.Log($"Failed on avatar url: {e.Url} FailedTotal= ({numberOfFailedDownloads})/({avatarBatchToLoad})");
            //Debug.Log($"Failed: {e.Message} FailedTotal= ({numberOfFailedDownloads})/({avatarBatchToLoad})");
            loading = false;
        }

        private float CalcAverageLoadingTime()
        {
            var sumLoadingTime = CalcSumLoadingTime();
            var avgLoadingTime = sumLoadingTime / avatars.Count;

            return avgLoadingTime;
        }

        private float CalcSumLoadingTime()
        {
            // If there are multiple avatar loaders loading asynchronously, return the time since the first avatar started loading
            if(maxAvatarLoaders > 1)
            {
                
                return Time.time - loadingStartTime;
            }
            float sumLoadingTime = 0;
            avatars.ForEach((avatar) => sumLoadingTime += avatar.LoadingTime);
            return sumLoadingTime;
        }
        
        private float CalcAverageDownloadSize(float currentFolderSize)
        {
            return currentFolderSize / avatars.Count;
        }

        private float GetTotalAvatarSize()
        {
            var totalSize = 0f;
            foreach (var avatar in avatars)
            {
                totalSize += AvatarCache.GetAvatarDataSizeInMb($"{avatar.AvatarID}/{AvatarCache.GetAvatarConfigurationHash(avatarConfig)}");
            }
            return totalSize;
        }
        private void OnDestroy()
        {
            avatarLoader?.Cancel();
        }
    }
}