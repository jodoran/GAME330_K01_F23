using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableAfterDelay : MonoBehaviour
{
    void Start()
    {
        // 3�� �Ŀ� DisableObject �޼��带 ȣ���մϴ�.
        Invoke("DisableObject", 3f);
    }

    void DisableObject()
    {
        // ���� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        gameObject.SetActive(false);
    }

}
