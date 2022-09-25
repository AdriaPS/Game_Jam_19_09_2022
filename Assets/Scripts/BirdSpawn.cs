using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BirdSpawn : MonoBehaviour
{
    public float interval;
    public float counter = 0;
    public GameObject bird;

    void Update()
    {
        if (counter < interval)
        {
            counter += Time.deltaTime;
        }
        else
        {
            spawnBird();
            counter = 0;
            interval = Random.Range(8, 17);
        }
    }

    public void spawnBird()
    {
        Vector2 randomPos = GetRandomPos();
        Instantiate(bird, randomPos, bird.transform.rotation);
    }

    public Vector2 GetRandomPos()
    {
        MeshCollider mc = GetComponent<MeshCollider>();
        float x, y;
        x = Random.Range(mc.bounds.min.x, mc.bounds.max.x);
        y = Random.Range(mc.bounds.min.y, mc.bounds.max.y);
        return new Vector2(x, y);
    }
}
