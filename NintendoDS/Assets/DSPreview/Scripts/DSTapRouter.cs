using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class DSTapRouter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    static GameObject BottomScreenCanvas;
    static Camera BottomScreenCam;

    // Start is called before the first frame update
    void Start()
    {
        BottomScreenCanvas = GameObject.Find("BottomScreenCanvas");
        BottomScreenCam = GameObject.Find("BottomScreenCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 ScreenPointToLocalRectanglePoint(Vector2 screenPoint)
    {
        Vector2 localCursor;
        RectTransform rect1 = GetComponent<RectTransform>();
        Vector2 pos1 = screenPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1, null, out localCursor))
            return Vector2.zero;

        return localCursor;
    }

    Vector2 ScreenPointToDSPoint(Vector2 screenPoint)
    {
        Vector2 localCursor = ScreenPointToLocalRectanglePoint(screenPoint);

        RectTransform rect1 = GetComponent<RectTransform>();
        float rectWidth = rect1.rect.width;
        float rectHeight = rect1.rect.height;
        localCursor += new Vector2(rectWidth * 0.5f, rectHeight * 0.5f);
        localCursor /= new Vector2(rectWidth, rectHeight);
        localCursor *= new Vector2(256.0f, 192.0f);

        return localCursor;
    }

    public static Vector2 TapPositionToBottomCanvasPosition(Vector2 tapPosition)
    {
        if(BottomScreenCanvas != null)
        {
            Vector2 localCursor = tapPosition;
            localCursor /= new Vector2(256.0f, 192.0f);
            localCursor *= new Vector2(388.0f, 291.0f);
            localCursor -= new Vector2(194.0f, 145.5f);

            RectTransform rect1 = BottomScreenCanvas.GetComponent<RectTransform>();
            if(rect1)
            {
                Vector2 transformedPosition = Vector2.zero;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, localCursor, null, out transformedPosition))
                {
                    return transformedPosition;
                }
            }
        }
        return Vector2.zero;
    }

    Vector2 ScreenPointToLocalPoint(Vector2 screenPos)
    {
        Vector2 normalizedDSScreenPosition = Vector2.zero;
        RectTransform rect1 = GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, screenPos, null, out normalizedDSScreenPosition);
        normalizedDSScreenPosition.x = normalizedDSScreenPosition.x / rect1.rect.width;
        normalizedDSScreenPosition.y = normalizedDSScreenPosition.y / rect1.rect.height;

        RectTransform canvasRect = BottomScreenCanvas.GetComponent<RectTransform>();
        Vector2 localCanvasPoint = normalizedDSScreenPosition;
        localCanvasPoint.x = localCanvasPoint.x * canvasRect.rect.width;
        localCanvasPoint.y = localCanvasPoint.y * canvasRect.rect.height;
        localCanvasPoint.x = localCanvasPoint.x + canvasRect.rect.width * 0.5f;
        localCanvasPoint.y = localCanvasPoint.y + canvasRect.rect.height * 0.5f;

        return localCanvasPoint;
    }

    public static bool RectangleContainsDSPoint(RectTransform testRect, Vector2 localCanvasPoint)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(testRect, localCanvasPoint, BottomScreenCam);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localCursor = ScreenPointToLocalPoint(eventData.position);
        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IDSTapListener>();
        foreach (IDSTapListener listener in listeners)
        {
            listener.OnScreenTapDown(localCursor);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 localCursor = ScreenPointToDSPoint(eventData.position);
        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IDSTapListener>();
        foreach (IDSTapListener listener in listeners)
        {
            listener.OnScreenTapUp(localCursor);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localCursor = ScreenPointToLocalPoint(eventData.position);
        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IDSTapListener>();
        foreach (IDSTapListener listener in listeners)
        {
            listener.OnScreenDrag(localCursor);
        }
    }
}
