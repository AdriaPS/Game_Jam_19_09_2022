using System;
using System.Linq;
using UnityEngine;

public class Water : MonoBehaviour
{
    public WaterState water;
    public WaterState ice;
    public WaterState vapor;

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
        }
        else if (water.prefab.activeSelf)
        {
            SetState(vapor);
        }
    }

    public void Freeze()
    {
        if (vapor.prefab.activeSelf)
        {
            SetState(water);
        }
        else if (water.prefab.activeSelf)
        {
            SetState(ice);
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