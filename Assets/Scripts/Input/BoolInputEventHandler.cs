using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(menuName = "Input/Event Handlers/Bool", fileName = nameof(BoolInputEventHandler))]
    public class BoolInputEventHandler : InputEventHandler<bool>
    {
        protected override bool GetValue(InputAction.CallbackContext ctx)
        {
            return ctx.ReadValueAsButton();
        }
    }
}