using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ReadyPlayerMe.Loadtest {
    public class AvatarIDReader
    {
        public List<string> AvatarList { get; private set; }
        public void FromCSV(string fileName)
        {
            AvatarList = new List<string>();
            StreamReader streamReader = new StreamReader(fileName);
            var data = streamReader.ReadToEnd();
            Debug.Log(data);
            AvatarList.AddRange(data?.Split(','));
        }
    }
}