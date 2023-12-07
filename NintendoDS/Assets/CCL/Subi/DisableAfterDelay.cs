using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableAfterDelay : MonoBehaviour
{
    void Start()
    {
        // 3초 후에 DisableObject 메서드를 호출합니다.
        Invoke("DisableObject", 3f);
    }

    void DisableObject()
    {
        // 게임 오브젝트를 비활성화합니다.
        gameObject.SetActive(false);
    }

}
