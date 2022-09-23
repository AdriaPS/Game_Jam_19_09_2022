using System;
using Codetox.Core;
using Codetox.Messaging;
using Codetox.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TemperatureWave : MonoBehaviour
{
    public enum Mode
    {
        Heat,
        Freeze
    }

    public Mode mode;
    public ValueReference<LayerMask> targetLayers;
    public ValueReference<float> radius;
    public ValueReference<float> time;
    public Ease expansionEase;
    public Ease effectEase;
    public GameObject sphere;
    public ValueReference<float> cooldown;
    public ValueReference<bool> isReady;
    
    public CameraShake cameraShake;

    public UnityEvent onStartWave;
    public UnityEvent onFinishWave;

    private void OnEnable()
    {
        if (!isReady.Value)
        {
            onFinishWave?.Invoke();
            return;
        }
        onStartWave?.Invoke();
        isReady.Value = false;
        Time.timeScale = 0;
        transform.localScale = Vector3.zero;
        StartCoroutine(cameraShake.Shake(.15f, .2f));
        transform.DOScale(Vector3.one * radius.Value * 2, time.Value).SetEase(expansionEase).SetUpdate(true)
            .OnComplete(() =>
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, radius.Value, targetLayers.Value);
                colliders.ForEach(c => ApplyTemperature(c.gameObject));
                Time.timeScale = 1;
                onFinishWave?.Invoke();
                DOVirtual.DelayedCall(cooldown.Value, () => isReady.Value = true);
            });
        var material = sphere.GetComponent<MeshRenderer>().material;
        material.SetInt("_isHeat", mode == Mode.Heat ? 1 : 0);
        material.SetFloat("_AlphaThreshold", 0);
        material.DOFloat(1, "_AlphaThreshold", time.Value).SetEase(effectEase).SetUpdate(true);
    }

    public void ApplyTemperature(GameObject target)
    {
        target.Send<Water>(w =>
        {
            switch (mode)
            {
                case Mode.Freeze:
                    w.Freeze();
                    break;
                case Mode.Heat:
                    w.Heat();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }, Scope.ParentsAndChildren);
    }
}