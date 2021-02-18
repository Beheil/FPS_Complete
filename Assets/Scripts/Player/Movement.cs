using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float gravity = -13f;
        [SerializeField] private float jumpSpeed = 3f;
        [SerializeField] private float dashSpeed = 10f;
        [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
            
        private float _velocityY = 0.0f;
        private bool _jumpPressed;
        
        private Vector2 _moveDirection;
        private Vector2 _currentDirection = Vector2.zero;
        private Vector2 _currentDirectionVelocity = Vector2.zero;
        
        private CharacterController _controller = null;

        private void Start() => _controller = GetComponent<CharacterController>();
        private void OnMove(InputValue value) => _moveDirection = value.Get<Vector2>();
        private void OnJump(InputValue value) => _jumpPressed = value.Get<float>() >= 1f;
        private void OnDash() => _currentDirection.y = dashSpeed;

        private void Update()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            if (_controller.isGrounded)
            {
                _velocityY = -2f;
            }

            _currentDirection = Vector2.SmoothDamp(_currentDirection, _moveDirection,
                ref _currentDirectionVelocity, moveSmoothTime);
            
            
            if (_controller.isGrounded && _jumpPressed)
            {
                _velocityY = Mathf.Sqrt(jumpSpeed * -2f * gravity);
            }
            
            
            _velocityY += gravity * Time.deltaTime;
            
            var velocity = (transform.forward * _currentDirection.y + transform.right * 
                _currentDirection.x) * moveSpeed + Vector3.up * _velocityY;

            _controller.Move(velocity * Time.deltaTime);
        }
    }
}