using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class AnimatorParameterController : MonoBehaviour
    {
        private static readonly int VelocityX = Animator.StringToHash("Velocity X");
        private static readonly int VelocityY = Animator.StringToHash("Velocity Y");
        private static readonly int InputX = Animator.StringToHash("Input X");
        private static readonly int IsWallGripping = Animator.StringToHash("Is Wall Gripping");

        public new Rigidbody2D rigidbody;
        public Animator animator;
        public Variable<Vector2> direction;
        public Variable<bool> isWallGripping;

        private void FixedUpdate()
        {
            animator.SetFloat(VelocityX, Mathf.Abs(rigidbody.velocity.x));
            animator.SetFloat(VelocityY, rigidbody.velocity.y);
            animator.SetFloat(InputX, Mathf.Abs(direction.Value.x));
            animator.SetBool(IsWallGripping, isWallGripping.Value);
        }
    }
}