using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.01f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.02f; // 흔들림 강도

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
            //다음 프레임까지 대기
            yield return null;
        }
        //흔들림이 끝나면 카메라의 위치를 원래 위치로 되돌림
        transform.localPosition = originalPosition;
    }
}
