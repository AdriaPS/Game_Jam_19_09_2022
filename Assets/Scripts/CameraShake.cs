using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float timer = 0.0f;

        while (timer < duration)
        {
            float posX = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(posX, originalPos.y, originalPos.z);

            timer += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
