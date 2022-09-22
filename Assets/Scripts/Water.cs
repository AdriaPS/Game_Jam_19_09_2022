using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    public WaterState water;
    public WaterState ice;
    public WaterState vapor;

    public UnityEvent onSetToWater;
    public UnityEvent onSetToIce;
    public UnityEvent onSetToVapor;

    private WaterState[] _states;

    private void OnEnable()
    {
        _states = new[] {water, ice, vapor};
        SetState(_states.First(s => s.defaultState));
    }

    public void Heat()
    {
        if (ice.prefab.activeSelf)
        {
            SetState(water);
            onSetToIce?.Invoke();
        }
        else if (water.prefab.activeSelf)
        {
            SetState(vapor);
            onSetToVapor?.Invoke();
        }
    }

    public void Freeze()
    {
        if (vapor.prefab.activeSelf)
        {
            SetState(water);
            onSetToWater?.Invoke();
        }
        else if (water.prefab.activeSelf)
        {
            SetState(ice);
            onSetToIce?.Invoke();
        }
    }

    private void SetState(WaterState waterState)
    {
        var states = new[] {water, ice, vapor};

        foreach (var state in states) state.prefab.SetActive(state.Equals(waterState));
    }

    [Serializable]
    public class WaterState
    {
        public GameObject prefab;
        public bool defaultState;
    }
}