using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTapPrinter : MonoBehaviour, IDSTapListener
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        Debug.Log("ScreenTapDown at " + tapPosition);
    }

    public void OnScreenDrag(Vector2 tapPosition)
    {
        Debug.Log("OnScreenDrag: " + tapPosition);
    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
        Debug.Log("ScreenTapUp at " + tapPosition);
    }
}
