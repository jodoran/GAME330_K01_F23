using UnityEngine;

public class ScreenTapPrinter : MonoBehaviour, IDSTapListener
{

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
