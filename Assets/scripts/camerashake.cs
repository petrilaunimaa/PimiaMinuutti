using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    public Vector3 offset;
    public float duration = 1.0f;

    // Juice generator
    public IEnumerator CameraShake(float magnitude)
    {
        Vector3 originalPos = offset;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            offset = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (elapsed >= duration)
        {
            offset = new Vector3(0, 0, 0);
        }
    }
    public void FixedUpdate()
    {
        transform.position = new Vector3(0,0,-10) + offset;
    }
}