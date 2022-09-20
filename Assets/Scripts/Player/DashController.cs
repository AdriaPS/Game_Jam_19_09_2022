using Codetox.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class DashController : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private ValueReference<float> distance;
        [SerializeField] private ValueReference<float> time;
        [SerializeField] private ValueReference<bool> isReady;
        [SerializeField] private Ease ease;

        public UnityEvent onDashStart;
        public UnityEvent onDashFinish;

        private bool _isDashing = true;

        private void Update()
        {
            if (!_isDashing && !isReady.Value) onDashFinish?.Invoke();
        }

        public void Dash()
        {
            if (!isReady.Value) return;

            var finalPosition = rigidbody.position.x + distance.Value * rigidbody.transform.right.x;

            isReady.Value = false;
            _isDashing = true;
            rigidbody.velocity = Vector2.zero;
            rigidbody.DOMoveX(finalPosition, time.Value).SetEase(ease).OnComplete(() => _isDashing = false);

            onDashStart?.Invoke();
        }
    }
}