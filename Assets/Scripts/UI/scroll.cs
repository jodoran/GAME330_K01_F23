using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour, IDSTapListener
{
    private bool isDragging = false;
    private Vector2 dragStartPosition;

    public ScrollRect scrollView;
    public float scrollSpeed;

    public void OnScreenDrag(Vector2 tapPosition)
    {
        if (isDragging)
        {
            // Calculate the drag distance
            Vector2 dragDelta = (Vector2)Input.mousePosition - dragStartPosition;

            // Apply the drag to the scroll view with the scrollSpeed
            Vector2 normalizedDelta = new Vector2(dragDelta.x / Screen.width, dragDelta.y / Screen.height);
            scrollView.normalizedPosition -= normalizedDelta * scrollSpeed;

            // Clamp the scroll position between 0 and 1
            scrollView.normalizedPosition = new Vector2(Mathf.Clamp01(scrollView.normalizedPosition.x), Mathf.Clamp01(scrollView.normalizedPosition.y));

            // Update the drag start position
            dragStartPosition = Input.mousePosition;
        }
    }

    public void OnScreenTapDown(Vector2 tapPosition)
    {
        isDragging = true;

        dragStartPosition = Input.mousePosition;
    }

    public void OnScreenTapUp(Vector2 tapPosition)
    {
        isDragging = false;
    }
}
