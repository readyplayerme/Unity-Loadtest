using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class ToggleLight : MonoBehaviour
    {
        [SerializeField] private GameObject DirectionalLight;

        private Button btnToggle;
        // Start is called before the first frame update
        void Start()
        {
            btnToggle = GetComponent<Button>();
            btnToggle.onClick.AddListener(OnToggleClick);
        }

        private void OnToggleClick()
        {
            DirectionalLight.SetActive(!DirectionalLight.activeSelf);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}