using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.2f; // 흔들림 강도

    private Vector3 originalPosition;

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
