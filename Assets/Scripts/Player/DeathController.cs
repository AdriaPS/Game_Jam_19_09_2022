using UnityEngine;

namespace Player
{
    public class DeathController : MonoBehaviour
    {
        public new Rigidbody2D rigidbody;
        public CameraShake cameraShake;

        private void OnEnable()
        {
            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector2.zero;
            StartCoroutine(cameraShake.Shake(.15f, .2f));
        }
    }
}