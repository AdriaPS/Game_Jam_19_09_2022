using Codetox.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class DashController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
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
                var finalPosition = rigidbody.position.x + distance.Value * rigidbody.transform.right.x;

                canDash.Value = false;
                isReady.Value = false;
                rigidbody.velocity = Vector2.zero;
                rigidbody.DOMoveX(finalPosition, time.Value).SetEase(ease).OnComplete(() =>
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
    }
}