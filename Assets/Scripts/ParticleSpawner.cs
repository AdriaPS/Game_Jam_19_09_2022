using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}