using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe.Core;

namespace ReadyPlayerMe.Loadtest
{
    public class Avatar : MonoBehaviour
    {
        public float LoadingTime { get; private set; }
        public AvatarMetadata Metadata { get; private set; }
        public string AvatarID { get; private set; }

        public void AvatarDownloaded(AvatarMetadata metadata, float avatarLoadingTime, string id)
        {
            Metadata = metadata;
            LoadingTime = avatarLoadingTime;
            AvatarID = id;
        }
    }
}