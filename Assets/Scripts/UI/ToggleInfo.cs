using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI 
{
    public class ToggleInfo : MonoBehaviour
    {
        private Button btnOpenInfo;
        [SerializeField] private GameObject pnlInfo;
        
        void Start()
        {
            btnOpenInfo = GetComponent<Button>();
            pnlInfo.SetActive(false);
            btnOpenInfo.onClick.AddListener(OnOpenInfoClick);
        }
        
        private void OnOpenInfoClick()
        {
            pnlInfo.SetActive(!pnlInfo.activeSelf);
        }
    }
}
