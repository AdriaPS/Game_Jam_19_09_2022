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

        public CameraShake cameraShake;

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

            StartCoroutine(cameraShake.Shake(.15f, .2f));
            
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

            if (x >= 0.25f && y >= 0.25f) return new Vector2(1, 1).normalized;
            if (x <= -0.25f && y >= 0.25f) return new Vector2(-1, 1).normalized;
            if (x >= 0.25f && y <= -0.25f) return new Vector2(1, -1).normalized;
            if (x <= -0.25f && y <= -0.25f) return new Vector2(-1, -1).normalized;
            if (x > 0f && y is > -0.25f and < 0.25f) return Vector2.right;
            if (x < 0f && y is > -0.25f and < 0.25f) return Vector2.left;
            if (y > 0f && x is > -0.25f and < 0.25f) return Vector2.up;
            if (y < 0f && x is > -0.25f and < 0.25f) return Vector2.down;

            return rigidbody.transform.right;
        }
    }
}