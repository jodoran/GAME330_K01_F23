using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public int speed;
    public float minX; // min x location
    public float maxX; // max x location
    public float h;
    public bool isBtnDown;

    public void Update()
    {
        Move();
    }

    public void Right()
    {
        h = 1;
        isBtnDown = true;
        print("Right");
    }

    public void Left()
    {
        h = -1;
        isBtnDown = true;
        print("Left");
    }

    public void BtnUp()
    {
        h = 0;
        isBtnDown = false;
        print("BtnUp");
    }

    public void Move()
    {
        if (isBtnDown)
        {
            Vector3 curPos = transform.position;

            curPos.x += h * speed * Time.deltaTime;

            curPos.x = Mathf.Clamp(curPos.x, minX, maxX);

            transform.position = curPos;
        }
    }
}
