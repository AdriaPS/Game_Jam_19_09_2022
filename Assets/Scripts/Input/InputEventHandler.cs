using Codetox;
using Codetox.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public abstract class InputEventHandler<T> : CustomScriptableObject where T : struct
    {
        [SerializeField] protected Event<T> onActionStartedEvent;
        [SerializeField] protected Event<T> onActionPerformedEvent;
        [SerializeField] protected Event<T> onActionCanceledEvent;

        public void OnInputEvent(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Started:
                    if (onActionStartedEvent) onActionStartedEvent.Invoke(GetValue(ctx));
                    break;
                case InputActionPhase.Performed:
                    if (onActionPerformedEvent) onActionPerformedEvent.Invoke(GetValue(ctx));
                    break;
                case InputActionPhase.Canceled:
                    if (onActionCanceledEvent) onActionCanceledEvent.Invoke(GetValue(ctx));
                    break;
            }
        }

        protected virtual T GetValue(InputAction.CallbackContext ctx)
        {
            return ctx.ReadValue<T>();
        }
    }
}