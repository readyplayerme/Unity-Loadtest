using UnityEngine;
 

namespace ReadyPlayerMe.Loadtest {
    public class FlyCamera : MonoBehaviour
    {
        [SerializeField] public float speed = 0.1f;
        [SerializeField] public float mouseSpeed = 2f;

        public bool IsEnabled { get; private set; }
        
        private Vector3 _angles;

        private void Update() {
            if (Input.GetButtonDown("Jump"))
            {
                ToggleFlyCamera();
            }

            if (IsEnabled)
            {
                _angles.x -= Input.GetAxis("Mouse Y") * mouseSpeed;
                _angles.y += Input.GetAxis("Mouse X") * mouseSpeed;
                transform.eulerAngles = _angles;
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
                _angles = transform.eulerAngles;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
