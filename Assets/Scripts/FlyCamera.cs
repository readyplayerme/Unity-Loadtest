using System;
using UnityEngine;

namespace ReadyPlayerMe.Loadtest {
    public class FlyCamera : MonoBehaviour
    {
        [SerializeField] public float speed = 0.1f;
        [SerializeField] public float mouseSpeed = 2f;
        
        public event EventHandler Enabled;
        public event EventHandler Disabled; 

        public bool IsEnabled { get; private set; }
        
        private Vector3 angles;

        private void LateUpdate() {
            if (Input.GetButtonDown("Jump"))
            {
                ToggleFlyCamera();
            }

            if (IsEnabled)
            {
                angles.x -= Input.GetAxis("Mouse Y") * mouseSpeed;
                angles.y += Input.GetAxis("Mouse X") * mouseSpeed;
                transform.eulerAngles = angles;
                transform.position +=
                    Input.GetAxis("Horizontal") * speed * transform.right +
                    Input.GetAxis("Vertical") * speed * transform.forward;
            }
        }
        
        private void ToggleFlyCamera()
        {
            IsEnabled = !IsEnabled;
            if (IsEnabled)
            {
                angles = transform.eulerAngles;
                Cursor.lockState = CursorLockMode.Locked;
                OnEnabled(EventArgs.Empty);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                OnDisabled(EventArgs.Empty);
            }
        }
        
        private void OnEnabled(EventArgs e)
        {
            EventHandler handler = Enabled;
            handler?.Invoke(this, e);
        }

        private void OnDisabled(EventArgs e)
        {
            EventHandler handler = Disabled;
            handler?.Invoke(this, e);
        }
    }
}
