using System;
using Codetox.Variables;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class MovementController
    {
        public Rigidbody2D rigidbody2D;
        public Variable<Vector2> direction;
        public ValueReference<float> speed;
        public ValueReference<float> smoothTime;

        private float _velocityX;

        public void Move()
        {
            var transform = rigidbody2D.transform;
            var currentVelocityX = rigidbody2D.velocity.x;
            var targetVelocityX = direction.Value.x * speed.Value;
            var finalVelocityX = Mathf.SmoothDamp(currentVelocityX, targetVelocityX, ref _velocityX, smoothTime.Value);

            rigidbody2D.velocity = new Vector2(finalVelocityX, rigidbody2D.velocity.y);

            transform.right = targetVelocityX switch
            {
                > 0f => Vector3.right,
                < 0f => Vector3.left,
                _ => transform.right
            };
        }
    }
}