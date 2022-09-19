using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class ToggleFullscreen : MonoBehaviour
    {
        private Button btnToggleFullscreen;
    
        void Start()
        {
            btnToggleFullscreen = GetComponent<Button>();
            btnToggleFullscreen.onClick.AddListener(OnToggleFullscreenClick);
        }

        private void OnToggleFullscreenClick()
        {
            Screen.fullScreen = !Screen.fullScreen;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}