using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour
{
    void Start()
    {
        // CanvasRenderer 컴포넌트 가져오기
        CanvasRenderer canvasRenderer = GetComponent<CanvasRenderer>();

        // 가장 맨 위로 배치 (sortingOrder를 다른 모든 UI 요소보다 크게 설정)
        canvasRenderer.sortingOrder = int.MaxValue;
    }
}

// Update is called once per frame
void Update()
    {
        
    }
}
