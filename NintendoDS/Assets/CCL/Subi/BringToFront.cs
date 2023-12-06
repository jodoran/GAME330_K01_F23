using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BringToFront : MonoBehaviour
{

    void Start()
    {
        // Canvas 컴포넌트 가져오기
        Canvas canvas = GetComponent<Canvas>();

        // Overlay 모드로 변경
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Order in Layer를 1로 설정 (다른 UI 요소보다 큰 숫자로 설정)
        canvas.sortingOrder = 1;
    }
}

// Update is called once per frame



