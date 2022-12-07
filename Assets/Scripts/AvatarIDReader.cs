using System;
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
            
            var txt = Resources.Load(fileName) as TextAsset;
            var data = txt?.text;
            
            AvatarList.AddRange(data?.Split(',') ?? Array.Empty<string>());
        }
    }
}