using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class DamageTaker : MonoBehaviour
    {
        public UnityEvent onTakeDamage;
        
        public void TakeDamage()
        {
            onTakeDamage?.Invoke();
        }
    }
}