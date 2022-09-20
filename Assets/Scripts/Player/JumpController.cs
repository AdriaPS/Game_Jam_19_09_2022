using System;
using System.Diagnostics.CodeAnalysis;
using Codetox.Variables;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class JumpController
    {
        public Rigidbody2D rigidbody2D;
        public ValueReference<float> minHeight;
        public ValueReference<float> maxHeight;
        public ValueReference<float> apexTime;
        public ValueReference<int> jumpAmount;

        private bool _isGrounded;
        private bool _isJumping;
        private int _jumpsLeft;

        [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
        public void ApplyGravity(float dt)
        {
            if (_isGrounded) return;

            var apexTime = this.apexTime.Value;
            var gravity = 2 * maxHeight.Value / (apexTime * apexTime);
            var minVelocity = 2 * minHeight.Value / apexTime;
            var velocity = rigidbody2D.velocity;

            if (!_isJumping && velocity.y > minVelocity) velocity.y = minVelocity;
            else velocity.y -= gravity * dt;

            rigidbody2D.velocity = velocity;
        }

        public void OnJump(bool isJumping)
        {
            if (!isJumping)
            {
                _isJumping = false;
                return;
            }

            if (_jumpsLeft <= 0) return;

            var maxVelocity = 2 * maxHeight.Value / apexTime.Value;

            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxVelocity);
            _isJumping = true;
            _jumpsLeft--;
        }

        public void OnGrounded(bool isGrounded)
        {
            _isGrounded = isGrounded;

            if (!_isGrounded) return;

            _jumpsLeft = jumpAmount.Value;
        }
    }
}