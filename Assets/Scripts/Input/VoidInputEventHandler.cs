using Codetox;
using Codetox.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(menuName = "Input/Event Handlers/Void", fileName = nameof(VoidInputEventHandler))]
    public class VoidInputEventHandler : CustomScriptableObject
    {
        [SerializeField] private VoidEvent onActionStartedEvent;
        [SerializeField] private VoidEvent onActionPerformedEvent;
        [SerializeField] private VoidEvent onActionCanceledEvent;

        public void OnInputEvent(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Started:
                    if (onActionStartedEvent) onActionStartedEvent.Invoke();
                    break;
                case InputActionPhase.Performed:
                    if (onActionPerformedEvent) onActionPerformedEvent.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    if (onActionCanceledEvent) onActionCanceledEvent.Invoke();
                    break;
            }
        }
    }
}