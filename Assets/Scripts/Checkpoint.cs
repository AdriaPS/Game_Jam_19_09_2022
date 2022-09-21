using System;
using System.Collections;
using System.Collections.Generic;
using Codetox.Variables;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Variable<GameObject> lastCheckpoint;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint.Value = col.gameObject;
        }
    }
}
