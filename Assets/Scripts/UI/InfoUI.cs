using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class InfoUI : MonoBehaviour
    {
        [SerializeField] private GameObject pnlInfo;
        [SerializeField] private Button btnClose;
        [SerializeField] private Button btnGitHub;
        [SerializeField] private Button btnReadyPlayerMe;
    
        void Start()
        {
            InitUI();
        }

        private void InitUI()
        {
            btnGitHub.onClick.AddListener(OnGitHubClick);
            btnReadyPlayerMe.onClick.AddListener(OnReadyPlayerMeClick);
            btnClose.onClick.AddListener(OnCloseClick);
        }

        private void OnCloseClick()
        {
            pnlInfo.SetActive(false);
        }

        private void OnReadyPlayerMeClick()
        {
            Application.OpenURL("https://readyplayer.me");
        }

        private void OnGitHubClick()
        {
            Application.OpenURL("https://github.com/readyplayerme/Unity-Loadtest");
        }
    }

}