using DG.Tweening;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay;

    private void OnEnable()
    {
        DOVirtual.DelayedCall(delay, () => Destroy(gameObject));
    }
}