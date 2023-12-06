using UnityEngine;


public class BringToFront : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
        // Canvas ������Ʈ ��������
        Canvas canvas = GetComponent<Canvas>();

        // Overlay ���� ����
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Order in Layer�� 1�� ���� (�ٸ� UI ��Һ��� ū ���ڷ� ����)
        canvas.sortingOrder = 1;
    }
}

// Update is called once per frame



