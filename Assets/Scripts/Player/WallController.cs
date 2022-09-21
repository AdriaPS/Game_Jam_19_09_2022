using Codetox.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class WallController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
        public ValueReference<Vector2> direction;
        public ValueReference<float> slidingSpeed;
        public ValueReference<float> minHeight;
        public ValueReference<float> maxHeight;
        public ValueReference<float> apexTime;
        public Variable<bool> isJumping;
        public Variable<bool> isGripping;
        public UnityEvent onLeaveWall;

        private float _gravity;
        private bool _isWallJumping;
        private float _maxVelocity;
        private float _minVelocity;

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
            var rigidbodyTransform = rigidbody.transform;
            var directionValue = direction.Value.x;

            rigidbodyTransform.right = directionValue switch
            {
                > 0f => Vector3.right,
                < 0f => Vector3.left,
                _ => rigidbodyTransform.right
            };

            var velocity = rigidbody.velocity;

            if (isGripping.Value && !_isWallJumping) velocity.y = 0f;
            else if (velocity.y > 0) velocity.y -= _gravity * Time.fixedDeltaTime;
            else velocity.y = -slidingSpeed.Value;

            rigidbody.velocity = velocity;
        }

        private void OnEnable()
        {
            isJumping.OnValueChanged += OnWallJump;
        }

        private void OnDisable()
        {
            isJumping.OnValueChanged -= OnWallJump;
        }

        private void OnWallJump(bool wannaJump)
        {
            if (!wannaJump) return;
            rigidbody.velocity = new Vector2(-rigidbody.transform.right.x * _maxVelocity * 0.75f, _maxVelocity);
            _isWallJumping = true;
            DOVirtual.DelayedCall(0.1f, () =>
            {
                _isWallJumping = false;
                onLeaveWall.Invoke();
            });
        }

        public void OnLeaveWall()
        {
            if (_isWallJumping) return;
            onLeaveWall?.Invoke();
        }
    }
}