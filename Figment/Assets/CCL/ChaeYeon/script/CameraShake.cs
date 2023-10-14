using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.01f; // ��鸲 ���� �ð�
    public float shakeMagnitude = 0.02f; // ��鸲 ����

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
            //���� �����ӱ��� ���
            yield return null;
        }
        //��鸲�� ������ ī�޶��� ��ġ�� ���� ��ġ�� �ǵ���
        transform.localPosition = originalPosition;
    }
}
