using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera = null;
        [SerializeField] private float mouseSensitivity = 3.5f;
        [SerializeField] [Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
        [SerializeField] private bool lockCursor = true;

        private Vector2 _lookVector;
        private Vector2 _currentMouseDelta = Vector2.zero;
        private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
        
        private float _cameraPitch = 0.0f;
        
        private void OnLook(InputValue value) => _lookVector = value.Get<Vector2>();

        private void Start()
        {
            if (!lockCursor) return;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            UpdateMouseLook();
            
            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                lockCursor = !lockCursor;
                // EditorApplication.isPlaying = false;
            }
        }

        private void UpdateMouseLook()
        {
            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, _lookVector,
                ref _currentMouseDeltaVelocity, mouseSmoothTime);
            
            _cameraPitch -= _currentMouseDelta.y * mouseSensitivity;

            _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
            
            playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
            
            transform.Rotate(Vector3.up * _currentMouseDelta.x * mouseSensitivity);
        }
    }
}