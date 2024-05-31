using System;

namespace ReadyPlayerMe.Loadtest
{
    public class AllAvatarsLoadedEventArgs : EventArgs
    {
        public float SumLoadingTime { get; }
        public float SumDownloadSize { get; }
        public float AverageDownloadSize { get; }
        public AllAvatarsLoadedEventArgs(float sumLoadingTime, float sumDownloadSize, float averageDownloadSize)
        {
            SumLoadingTime = sumLoadingTime;
            SumDownloadSize = sumDownloadSize;
            AverageDownloadSize = averageDownloadSize;
        }
    }
    public class AvatarLoadedEventArgs : AllAvatarsLoadedEventArgs
    {
        public Avatar Avatar { get; }
        public float AverageLoadingTime { get; }
        public float AverageDownloadSize { get; }

        public AvatarLoadedEventArgs(Avatar avatar, float averageLoadingTime, float sumLoadingTime, float averageDownloadSize, float sumDownloadSize) : base(sumLoadingTime, sumDownloadSize, averageDownloadSize)
        {
            Avatar = avatar;
            AverageLoadingTime = averageLoadingTime;
        }
    }
}