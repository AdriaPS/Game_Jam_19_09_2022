using Codetox.Misc;
using Codetox.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class DashController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
        public Variable<Vector2> direction;
        public ValueReference<float> distance;
        public ValueReference<float> time;
        public ValueReference<float> cooldown;
        public ValueReference<bool> canDash;
        public ValueReference<bool> isReady;
        public Ease ease;

        public UnityEvent onDashStart;
        public UnityEvent onDashFinish;

        private void OnEnable()
        {
            Dash();
        }

        public void Dash()
        {
            if (canDash.Value && isReady.Value)
            {
                var dashDirection = GetDashDirection();
                var finalPosition = rigidbody.position + distance.Value * (Vector2) transform.right;

                canDash.Value = false;
                isReady.Value = false;
                rigidbody.velocity = Vector2.zero;
                rigidbody.DOMove(finalPosition, time.Value).SetEase(ease).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(cooldown.Value, () => isReady.Value = true);
                    onDashFinish?.Invoke();
                });

                onDashStart?.Invoke();
            }
            else
            {
                onDashFinish?.Invoke();
            }
        }

        private Vector2 GetDashDirection()
        {
            var dir = direction.Value;
            var x = dir.x;
            var y = dir.y;

            if (x is >= 0.25f and <= 1f && y is >= 0.25f and <= 1f) return new Vector2(1, 1).normalized;
            if (x is >= -1 and <= -0.25f && y is >= 0.25f and <= 1f) return new Vector2(-1, 1).normalized;
            if (x is >= 0.25f and <= 1f && y is >= -1f and <= -0.25f) return new Vector2(1, -1).normalized;
            if (x is >= -1f and <= -0.25f && y is >= -1f and <= 0.25f) return new Vector2(-1, -1).normalized;
            if (x is >= -0.25f and <= 0.25f && y is >= 0f and <= 1f) return Vector2.up;
            if (x is >= -0.25f and <= 0.25f && y is >= -1f and <= 0f) return Vector2.down;
            if (x is >= 0f and <= 1f && y is >= -0.25f and <= 0.25f) return Vector2.right;
            if (x is >= -1f and <= 0f && y is >= -0.25f and <= 0.25f) return Vector2.left;
            
            return Vector2.right;
        }
    }
}