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
    public Ease expansionEase;
    public Ease effectEase;
    public GameObject sphere;

    private void OnEnable()
    {
        Time.timeScale = 0;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * radius.Value * 2, time.Value).SetEase(expansionEase).SetUpdate(true)
            .OnComplete(() =>
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, radius.Value, targetLayers.Value);
                colliders.ForEach(c => ApplyTemperature(c.gameObject));
                Time.timeScale = 1;
                gameObject.SetActive(false);
            });
        var material = sphere.GetComponent<MeshRenderer>().material;
        material.SetInt("_isHeat", mode == Mode.Heat ? 1 : 0);
        material.SetFloat("_FresnelPower", 0f);
        material.DOFloat(5f, "_FresnelPower", time.Value).SetEase(effectEase).SetUpdate(true);
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