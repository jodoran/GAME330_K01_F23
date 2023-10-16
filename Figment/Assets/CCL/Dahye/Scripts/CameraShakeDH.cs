using System.Collections;
using UnityEngine;

public class CameraShakeDH : MonoBehaviour
{

    [Tooltip("흔들림 지속 시간")]
    [SerializeField] private float shakeDuration = 0.01f;

    [Tooltip("흔들림 강도")]
    [SerializeField] private float shakeMagnitude = 0.02f;

    private Vector3 originalPosition;

    private static CameraShakeDH instance;
    public static CameraShakeDH Instance { get { return instance; } }

    private void Awake()
    {
        // 이미 인스턴스가 존재하지 않는 경우
        if (instance == null)
        {
            // 현재 씬에서 UnitManager를 찾음
            var cam = FindObjectOfType<CameraShakeDH>();
            if (cam == null)
            {
                // UnitManager가 없으면 새로운 게임 오브젝트를 생성하고 UnitManager 컴포넌트를 추가
                GameObject obj = new GameObject("#Camera");
                cam = obj.AddComponent<CameraShakeDH>();
            }
            // 인스턴스 변수에 찾아낸 manager를 할당
            instance = cam;
            // 씬 전환 시 파괴되지 않도록 설정
            DontDestroyOnLoad(instance);
        }
    }

    /// <summary>
    /// 카메라를 흔드는 코루틴을 시작합니다.
    /// </summary>
    public void Shake()
    {
        Debug.Log("카메라 흔들려");
        StartCoroutine(StartShake());
    }

    /// <summary>
    /// 카메라 흔들기를 시작합니다.
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
            //다음 프레임까지 대기
            yield return null;
        }
        //흔들림이 끝나면 카메라의 위치를 원래 위치로 되돌림
        transform.localPosition = originalPosition;
    }
}
