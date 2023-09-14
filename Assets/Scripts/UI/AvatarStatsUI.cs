using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class AvatarStatsUI : MonoBehaviour
    {
        [SerializeField] private Text txtAvgLoadingTime;
        [SerializeField] private Text txtTotalLoadingTime;
        [SerializeField] private Text txtLastLoadingTime;
        [SerializeField] private Text txtCountAvatars;

        [SerializeField] private AvatarLoadingHandler avatarLoadingHandler;
        
        private int numberOfAvatarsLoaded = 0;
        
        void Start()
        {
            avatarLoadingHandler.AvatarLoaded += OnAvatarLoaded;
            avatarLoadingHandler.AllAvatarsLoaded += OnAllAvatarsLoaded;
        }
        
        private void OnAllAvatarsLoaded(object sender, AllAvatarsLoadedEventArgs args)
        {
            txtTotalLoadingTime.text = $"{args.SumLoadingTime:0.00} s";
        }

        private void OnAvatarLoaded(object sender, AvatarLoadedEventArgs args)
        {
            txtAvgLoadingTime.text = $"{args.AverageLoadingTime:0.00} s";
            txtLastLoadingTime.text = $"{args.Avatar.LoadingTime:0.00} s";
            txtTotalLoadingTime.text = $"{args.SumLoadingTime:0.00} s";

            numberOfAvatarsLoaded++;
            txtCountAvatars.text = numberOfAvatarsLoaded.ToString();
        }

        private void OnDestroy()
        {
            if (avatarLoadingHandler == null) return;
            avatarLoadingHandler.AvatarLoaded -= OnAvatarLoaded;
            avatarLoadingHandler.AllAvatarsLoaded -= OnAllAvatarsLoaded;
        }
    }
}
