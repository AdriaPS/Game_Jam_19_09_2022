using Codetox.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private ValueReference<float> minHeight;
        [SerializeField] private ValueReference<float> maxHeight;
        [SerializeField] private ValueReference<float> apexTime;
        [SerializeField] private ValueReference<int> jumpAmount;

        public UnityEvent onJump;

        private float _gravity, _maxVelocity, _minVelocity;
        private bool _isGrounded, _isJumping;
        private int _jumpsLeft;

        private void Awake()
        {
            var maxHeightValue = maxHeight.Value;
            var minHeightValue = minHeight.Value;
            var apexTimeValue = apexTime.Value;

            _gravity = 2 * maxHeightValue / (apexTimeValue * apexTimeValue);
            _maxVelocity = 2 * maxHeightValue / apexTimeValue;
            _minVelocity = 2 * minHeightValue / apexTimeValue;
        }

        private void FixedUpdate()
        {
            if (_isGrounded) return;

            var velocity = rigidbody.velocity;

            if (!_isJumping && velocity.y > _minVelocity) velocity.y = _minVelocity;
            else velocity.y -= _gravity * Time.fixedDeltaTime;

            rigidbody.velocity = velocity;
        }

        public void Jump(bool isJumping)
        {
            if (!isJumping)
            {
                _isJumping = false;
                return;
            }

            if (_jumpsLeft <= 0) return;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, _maxVelocity);
            onJump?.Invoke();
            _isJumping = true;
            _jumpsLeft--;
        }

        public void OnTouchGround(bool isGrounded)
        {
            if (!isGrounded)
            {
                _isGrounded = false;
                return;
            }

            _isGrounded = true;
            _jumpsLeft = jumpAmount.Value;
        }
    }
}