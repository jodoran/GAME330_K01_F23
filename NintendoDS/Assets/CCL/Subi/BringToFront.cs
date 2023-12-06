using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour
{
    void Start()
    {
        // CanvasRenderer ������Ʈ ��������
        CanvasRenderer canvasRenderer = GetComponent<CanvasRenderer>();

        // ���� �� ���� ��ġ (sortingOrder�� �ٸ� ��� UI ��Һ��� ũ�� ����)
        canvasRenderer.sortingOrder = int.MaxValue;
    }
}

// Update is called once per frame
void Update()
    {
        
    }
}
