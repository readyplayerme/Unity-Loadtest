using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class FlyCameraUI : MonoBehaviour
    {
        [SerializeField] private FlyCamera flyCamera;

        [SerializeField] private Text txtEnterFlyCam;
        [SerializeField] private Text txtFlyCamControls;
    
        // Update is called once per frame
        void Update()
        {
            txtEnterFlyCam.gameObject.SetActive(!flyCamera.IsEnabled);
            txtFlyCamControls.gameObject.SetActive(flyCamera.IsEnabled);
        }
    }
}
