using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ReadyPlayerMe.Loadtest {
    public class AvatarIDReader
    {
        public List<string> AvatarList { get; private set; }

        public void ReadCSVFromResources(string fileName)
        {
            AvatarList = new List<string>();
            string data = "";
            #if UNITY_WEBGL
                var txt = Resources.Load(fileName) as TextAsset;
                data = txt.text;
            #else
                var path = $"{Application.dataPath}/Resources/{fileName}";
                var streamReader = new StreamReader(fileName);
                data = streamReader.ReadToEnd();
            #endif
            
            AvatarList.AddRange(data?.Split(','));
        }
    }
}