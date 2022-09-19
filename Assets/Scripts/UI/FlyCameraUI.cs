using System;
using UnityEngine;
using UnityEngine.UI;

namespace ReadyPlayerMe.Loadtest.UI
{
    public class FlyCameraUI : MonoBehaviour
    {
        [SerializeField] private FlyCamera flyCamera;

        [SerializeField] private Text txtEnterFlyCam;
        [SerializeField] private Text txtFlyCamControls;


        private void Start()
        {
            flyCamera.Enabled += OnEnabled;
            flyCamera.Disabled += OnDisabled;
        }

        private void OnEnabled(System.Object sender, EventArgs e)
        {
            txtEnterFlyCam.gameObject.SetActive(!flyCamera.IsEnabled);
            txtFlyCamControls.gameObject.SetActive(flyCamera.IsEnabled);
        }

        private void OnDisabled(System.Object sender, EventArgs e)
        {
            txtEnterFlyCam.gameObject.SetActive(!flyCamera.IsEnabled);
            txtFlyCamControls.gameObject.SetActive(flyCamera.IsEnabled);
        }
    }
}
