using System.Collections;
using UnityEngine;

public class CameraShakeDH : MonoBehaviour
{

    [Tooltip("��鸲 ���� �ð�")]
    [SerializeField] private float shakeDuration = 0.01f;

    [Tooltip("��鸲 ����")]
    [SerializeField] private float shakeMagnitude = 0.02f;

    private Vector3 originalPosition;

    private static CameraShakeDH instance;
    public static CameraShakeDH Instance { get { return instance; } }

    private void Awake()
    {
        // �̹� �ν��Ͻ��� �������� �ʴ� ���
        if (instance == null)
        {
            // ���� ������ UnitManager�� ã��
            var cam = FindObjectOfType<CameraShakeDH>();
            if (cam == null)
            {
                // UnitManager�� ������ ���ο� ���� ������Ʈ�� �����ϰ� UnitManager ������Ʈ�� �߰�
                GameObject obj = new GameObject("#Camera");
                cam = obj.AddComponent<CameraShakeDH>();
            }
            // �ν��Ͻ� ������ ã�Ƴ� manager�� �Ҵ�
            instance = cam;
            // �� ��ȯ �� �ı����� �ʵ��� ����
            DontDestroyOnLoad(instance);
        }
    }

    /// <summary>
    /// ī�޶� ���� �ڷ�ƾ�� �����մϴ�.
    /// </summary>
    public void Shake()
    {
        Debug.Log("ī�޶� ����");
        StartCoroutine(StartShake());
    }

    /// <summary>
    /// ī�޶� ���⸦ �����մϴ�.
    /// </summary>
    private IEnumerator StartShake()
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
