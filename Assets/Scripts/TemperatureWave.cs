using System;
using Codetox.Core;
using Codetox.Messaging;
using Codetox.Variables;
using DG.Tweening;
using UnityEngine;

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
    public Ease ease;

    private void OnEnable()
    {
        Time.timeScale = 0;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * radius.Value * 2, time.Value).SetEase(ease).SetUpdate(true)
            .OnComplete(() =>
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, radius.Value, targetLayers.Value);
                colliders.ForEach(c => ApplyTemperature(c.gameObject));
                Time.timeScale = 1;
                gameObject.SetActive(false);
            });
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