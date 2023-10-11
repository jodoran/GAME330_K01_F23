using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public PlayerMovement player;
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        player = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Scroll();
    }

    public void Move()
    {
        Vector3 curPos = transform.position;
        curPos += Vector3.down;
        transform.position = curPos;
    }

    void Scroll()
    {
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //#.Sprites ReUse
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;

            //#.Cursor Index Change
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1) == -1 ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
