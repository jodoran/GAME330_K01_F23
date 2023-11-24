using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDSTapListener
{
    void OnScreenTapDown(Vector2 tapPosition);
    void OnScreenDrag(Vector2 tapPosition);
    void OnScreenTapUp(Vector2 tapPosition);
}
