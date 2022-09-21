using Codetox.Messaging;
using UnityEngine;

namespace Health
{
    public class DamageDealer : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.Send<DamageTaker>(damageTaker => damageTaker.TakeDamage());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.Send<DamageTaker>(damageTaker => damageTaker.TakeDamage());
        }
    }
}