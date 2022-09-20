using Codetox.Attributes;
using Codetox.Variables;
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
        public ValueReference<int> jumpAmount;
        
        [SerializeField] [Disabled] private bool isGrounded;
        [SerializeField] [Disabled] private bool isJumping;

        public UnityEvent onJump;

        private float _gravity, _maxVelocity, _minVelocity;
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

        private void OnDisable()
        {
            isGrounded = false;
            isJumping = false;
        }

        private void FixedUpdate()
        {
            if (isGrounded) return;

            var velocity = rigidbody.velocity;

            if (!isJumping && velocity.y > _minVelocity) velocity.y = _minVelocity;
            else velocity.y -= _gravity * Time.fixedDeltaTime;

            rigidbody.velocity = velocity;
        }

        public void Jump(bool isJumping)
        {
            if (!isJumping)
            {
                this.isJumping = false;
                return;
            }

            if (!isGrounded) return;
            
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, _maxVelocity);
            this.isJumping = true;
            onJump?.Invoke();
            //_jumpsLeft--;
        }

        public void OnTouchGround(bool isGrounded)
        {
            this.isGrounded = isGrounded;
            //_jumpsLeft = jumpAmount.Value;
        }
    }
}