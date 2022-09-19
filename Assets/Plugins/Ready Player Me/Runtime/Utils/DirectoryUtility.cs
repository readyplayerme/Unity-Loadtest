using System.IO;
using System.Linq;
using UnityEngine;

namespace ReadyPlayerMe
{
    public static class DirectoryUtility
    {
        /// The directory where avatar files will be downloaded.
        public static string DefaultAvatarFolder { get; set; } = "Avatars";

        public static void ValidateAvatarSaveDirectory(string guid, bool saveInProjectFolder = false)
        {
            var path = GetAvatarSaveDirectory(guid, saveInProjectFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetAvatarSaveDirectory(string guid, bool saveInProjectFolder = false) => $"{GetAvatarsDirectoryPath(saveInProjectFolder)}/{guid}";

        public static string GetRelativeProjectPath(string guid) => $"Assets/{DefaultAvatarFolder}/{guid}";

        /// Is there any avatars present in the persistent cache.
        public static bool IsCacheEmpty()
        {
            var path = GetAvatarsDirectoryPath();
            return !Directory.Exists(path) ||
                   (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0);
        }

        /// Clears the avatars from the persistent cache.
        public static void ClearAvatarsCache()
        {
            var path = GetAvatarsDirectoryPath();
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// Total Avatars stored in persistent cache.
        public static int GetAvatarCount()
        {
            var path = GetAvatarsDirectoryPath();
            return !Directory.Exists(path) ? 0 : new DirectoryInfo(path).GetDirectories().Length;

        }

        /// Total size of avatar stored in persistent cache. Returns total bytes.
        public static long GetCacheSize()
        {
            var path = GetAvatarsDirectoryPath();
            return !Directory.Exists(path) ? 0 : GetDirectorySize(new DirectoryInfo(path));
        }

        private static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            // Add file sizes.
            var fileInfos = directoryInfo.GetFiles();
            var size = fileInfos.Sum(fi => fi.Length);

            // Add subdirectory sizes.
            var directoryInfos = directoryInfo.GetDirectories();
            size += directoryInfos.Sum(GetDirectorySize);
            return size;
        }

        public static string GetAvatarsDirectoryPath(bool saveInProjectFolder = false)
        {
            var directory = saveInProjectFolder ? Application.dataPath : Application.persistentDataPath;
            return $"{directory}/{DefaultAvatarFolder}";
        }


    }
}
