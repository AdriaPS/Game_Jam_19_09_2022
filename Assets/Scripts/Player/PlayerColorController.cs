using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class PlayerColorController : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Variable<bool> canDash;
        public Variable<bool> dashIsReady;

        private void OnEnable()
        {
            canDash.OnValueChanged += CanDashOnValueChanged;
            dashIsReady.OnValueChanged += DashIsReadyOnValueChanged;
        }

        private void OnDisable()
        {
            canDash.OnValueChanged -= CanDashOnValueChanged;
            dashIsReady.OnValueChanged -= DashIsReadyOnValueChanged;
        }

        private void DashIsReadyOnValueChanged(bool condition)
        {
            if (condition && canDash.Value)
            {
                spriteRenderer.material.SetInt("_isColorChanged", 0);
                spriteRenderer.material.SetInt("_NoDash", 0);
            }
            else
            {
                spriteRenderer.material.SetInt("_isColorChanged", 1);
                spriteRenderer.material.SetInt("_NoDash", 1);
            }
        }

        private void CanDashOnValueChanged(bool condition)
        {
            if (condition && dashIsReady.Value)
            {
                spriteRenderer.material.SetInt("_isColorChanged", 0);
                spriteRenderer.material.SetInt("_NoDash", 0);
            }
            else
            {
                spriteRenderer.material.SetInt("_isColorChanged", 1);
                spriteRenderer.material.SetInt("_NoDash", 1);
            }
        }
    }
}