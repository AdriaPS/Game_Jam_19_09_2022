using Codetox.Variables;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class JumpController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
        public ValueReference<float> minHeight;
        public ValueReference<float> maxHeight;
        public ValueReference<float> apexTime;
        public ValueReference<float> coyoteTime;
        public Variable<bool> isJumping;
        public Variable<bool> isGrounded;
        public Variable<bool> canJump;
        public UnityEvent onJump;
        public ParticleSpawner particleSpawner;

        private float _gravity, _maxVelocity, _minVelocity;
        private bool _isInCoyoteTime;

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
            if (isGrounded.Value) return;

            var velocity = rigidbody.velocity;

            if (!isJumping.Value && velocity.y > _minVelocity) velocity.y = _minVelocity;
            else if (velocity.y < -_maxVelocity) velocity.y = -_maxVelocity;
            else velocity.y -= _gravity * Time.fixedDeltaTime;

            rigidbody.velocity = velocity;
        }

        private void OnEnable()
        {
            _isInCoyoteTime = false;
            isGrounded.Value = false;
            isJumping.OnValueChanged += OnJump;
            isGrounded.OnValueChanged += OnGrounded;
        }

        private void OnDisable()
        {
            isJumping.OnValueChanged -= OnJump;
            isGrounded.OnValueChanged -= OnGrounded;
        }

        private void OnJump(bool wannaJump)
        {
            if (wannaJump && (isGrounded.Value || _isInCoyoteTime || canJump.Value))
            {
                canJump.Value = false;
                _isInCoyoteTime = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, _maxVelocity);
                onJump?.Invoke();
            }
            if (isGrounded.Value)
            {
                particleSpawner.Spawn();
            }
        }

        private void OnGrounded(bool condition)
        {
            if (condition) return;
            DOVirtual
                .DelayedCall(coyoteTime.Value, () => _isInCoyoteTime = false)
                .OnStart(() => _isInCoyoteTime = true);
        }
    }
}