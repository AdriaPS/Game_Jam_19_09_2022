using DG.Tweening;
using UnityEngine;

public class FallingIce : MonoBehaviour
{
    public Vector3 shakeStrength;
    public int vibrato;
    public float fallDelay;
    public float fallVelocity;
    public Vector3 finalPosition;

    private bool _canFall = true;

    public void StartFalling()
    {
        if (!_canFall) return;
        _canFall = false;
        transform.DOShakePosition(fallDelay, shakeStrength, vibrato).OnComplete(() =>
            transform.DOMove(finalPosition, fallVelocity).SetSpeedBased(true).SetEase(Ease.InSine));
    }
}