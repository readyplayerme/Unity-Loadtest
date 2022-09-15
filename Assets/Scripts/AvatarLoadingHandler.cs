using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ReadyPlayerMe.Loadtest
{
    public class AvatarLoadingHandler : MonoBehaviour
    {
        [SerializeField] private string baseUrl = "https://api.readyplayer.me/v1/avatars/";
        [SerializeField] private GameObject loadingPlaceholder;

        private readonly List<float> avatarLoadingTimes = new List<float>();
        private float loadingTime = 0;
        private Vector3 loadingPosition;
        private bool loading = false;
        private GameObject placeholderAvatar;
        private AvatarLoader avatarLoader;
        
        public event EventHandler<AvatarLoadedEventArgs> AvatarLoaded;
        public event EventHandler<AllAvatarsLoadedEventArgs> AllAvatarsLoaded; 

        private void Update()
        {
            loadingTime += Time.deltaTime;
        }

        public void LoadAvatars(int numberOfAvatarsToLoad, AvatarConfig avatarConfig)
        {
            StartCoroutine(LoadAvatars(numberOfAvatarsToLoad, avatarConfig, AvatarIDs.AvatarList));
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

        private IEnumerator LoadAvatars(int numberOfAvatarsToLoad, AvatarConfig avatarConfig, List<string> avatarList)
        {
            for (var i = 0; (i < numberOfAvatarsToLoad && i < avatarList.Count) ; i++)
            {
                var avatarID = avatarList[i];
                loading = true;
                loadingTime = 0;
                
                avatarLoader = new AvatarLoader();
                avatarLoader.AvatarConfig = avatarConfig;
                
                loadingPosition = Quaternion.Euler(90, 0, 0) * Random.insideUnitCircle * 15; //random position in a circle with r=15
                placeholderAvatar = Instantiate(loadingPlaceholder);
                placeholderAvatar.transform.position = loadingPosition;
                
                avatarLoader.OnCompleted += OnLoadingCompleted;
                avatarLoader.OnFailed += OnLoadingFailed;
 
                avatarLoader.LoadAvatar(GetAvatarUrl(avatarID));
                
                yield return new WaitUntil(() => !loading);
            }
   
            OnAllAvatarsLoaded(new AllAvatarsLoadedEventArgs(CalcSumLoadingTime()));
        }
        
        private void OnLoadingCompleted(object sender, CompletionEventArgs args)
        {
            Destroy(placeholderAvatar);
            args.Avatar.transform.position = loadingPosition;
            //args.Avatar.SetActive(true);
            
            avatarLoadingTimes.Add(loadingTime);
            
            OnAvatarLoaded(new AvatarLoadedEventArgs(loadingTime, CalcAverageLoadingTime(), CalcSumLoadingTime()));
                
            loading = false;
        }

        private string GetAvatarUrl(string avatarID)
        {
            return baseUrl + avatarID + ".glb";
        }

        private void OnLoadingFailed(object sender, FailureEventArgs e)
        {
            Debug.Log("Failed: " + e.Message);
            loading = false;
        }

        private float CalcAverageLoadingTime()
        {
            var sumLoadingTime = CalcSumLoadingTime();
            var avgLoadingTime = sumLoadingTime / avatarLoadingTimes.Count;

            return avgLoadingTime;
        }

        private float CalcSumLoadingTime()
        {
            float sumLoadingTime = 0;
            avatarLoadingTimes.ForEach((time) => sumLoadingTime = sumLoadingTime + time);
            return sumLoadingTime;
        }

        private void OnDestroy()
        {
            avatarLoader?.Cancel();
        }
    }
    
    public class AllAvatarsLoadedEventArgs : EventArgs
    {
        public float SumLoadingTime { get; }

        public AllAvatarsLoadedEventArgs(float sumLoadingTime)
        {
            SumLoadingTime = sumLoadingTime;
        }
    }
    public class AvatarLoadedEventArgs : AllAvatarsLoadedEventArgs
    {
        public float LoadingTime { get; }
        public float AverageLoadingTime { get; }

        public AvatarLoadedEventArgs(float loadingTime, float averageLoadingTime, float sumLoadingTime) : base(sumLoadingTime)
        {
            LoadingTime = loadingTime;
            AverageLoadingTime = averageLoadingTime;
        }
    }
}