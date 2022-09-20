using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
        public ValueReference<Vector2> direction;
        public ValueReference<float> speed;
        public ValueReference<float> smoothTime;

        private float _currentVelocity;

        private void FixedUpdate()
        {
            var rigidbodyTransform = rigidbody.transform;
            var directionValue = direction.Value.x;
            var currentVelocity = rigidbody.velocity;
            var targetVelocity = directionValue * speed.Value;
            var finalVelocity = Mathf.SmoothDamp(currentVelocity.x, targetVelocity, ref _currentVelocity, smoothTime.Value);

            rigidbody.velocity = new Vector2(finalVelocity, currentVelocity.y);

            rigidbodyTransform.right = directionValue switch
            {
                > 0f => Vector3.right,
                < 0f => Vector3.left,
                _ => rigidbodyTransform.right
            };
        }
    }
}