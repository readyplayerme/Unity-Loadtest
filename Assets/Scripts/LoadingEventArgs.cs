using System;

namespace ReadyPlayerMe.Loadtest
{
    public class AllAvatarsLoadedEventArgs : EventArgs
    {
        public float SumLoadingTime { get; }
        public float SumDownloadSize { get; }

        public AllAvatarsLoadedEventArgs(float sumLoadingTime, float sumDownloadSize)
        {
            SumLoadingTime = sumLoadingTime;
            SumDownloadSize = sumDownloadSize;
        }
    }
    public class AvatarLoadedEventArgs : AllAvatarsLoadedEventArgs
    {
        public Avatar Avatar { get; }
        public float AverageLoadingTime { get; }
        public float AverageDownloadSize { get; }

        public AvatarLoadedEventArgs(Avatar avatar, float averageLoadingTime, float sumLoadingTime, float averageDownloadSize, float sumDownloadSize) : base(sumLoadingTime, sumDownloadSize)
        {
            Avatar = avatar;
            AverageLoadingTime = averageLoadingTime;
            AverageDownloadSize = averageDownloadSize;
        }
    }
}