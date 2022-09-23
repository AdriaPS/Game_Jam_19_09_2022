using DG.Tweening;
using UnityEngine;

public class FallingIce : MonoBehaviour
{
    public Vector3 shakeStrength;
    public int vibrato;
    public float fallDelay;
    public float fallVelocity;
    public Vector3 finalPosition;
    
    public CameraShake cameraShake;

    private bool _canFall = true;

    public void StartFalling()
    {
        if (!_canFall) return;
        StartCoroutine(cameraShake.Shake(.15f, .2f));
        _canFall = false;
        transform.DOShakePosition(fallDelay, shakeStrength, vibrato).OnComplete(() =>
            transform.DOMove(finalPosition, fallVelocity).SetSpeedBased(true).SetEase(Ease.InSine));
    }
}