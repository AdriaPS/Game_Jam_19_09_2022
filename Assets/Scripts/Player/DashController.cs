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

        private Tweener tween;

        private void OnDisable()
        {
            tween?.Kill();
        }

        public void Dash()
        {
            if (!canDash.Value || !isReady.Value)
            {
                onDashFinish?.Invoke();
                return;
            }

            var finalPosition = rigidbody.position + distance.Value * GetDashDirection();

            canDash.Value = false;
            isReady.Value = false;
            rigidbody.velocity = Vector2.zero;
            tween = rigidbody
                .DOMove(finalPosition, time.Value)
                .SetEase(ease).OnStart(() => onDashStart?.Invoke())
                .OnComplete(() => { onDashFinish?.Invoke(); });
            DOVirtual.DelayedCall(time.Value + cooldown.Value, () => isReady.Value = true);
        }

        private Vector2 GetDashDirection()
        {
            var dir = direction.Value;
            var x = dir.x;
            var y = dir.y;

            return x switch
            {
                >= 0.20f and <= 0.80f when y is >= 0.20f and <= 0.80f => new Vector2(1, 1).normalized,
                >= -0.80f and <= -0.20f when y is >= 0.20f and <= 0.80f => new Vector2(-1, 1).normalized,
                >= 0.20f and <= 0.80f when y is >= -0.80f and <= -0.20f => new Vector2(1, -1).normalized,
                >= -0.80f and <= -0.20f when y is >= -0.80f and <= -0.20f => new Vector2(-1, -1).normalized,
                _ => y switch
                {
                    > 0.80f => Vector2.up,
                    < -0.80f => Vector2.down,
                    _ => x switch
                    {
                        > 0.80f => Vector2.right,
                        < -0.80f => Vector2.left,
                        _ => rigidbody.transform.right
                    }
                }
            };
        }
    }
}