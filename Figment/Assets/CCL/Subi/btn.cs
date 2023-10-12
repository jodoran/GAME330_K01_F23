using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn : MonoBehaviour
{
    public Sprite[] sprites;
    public bool isClicked;

    public void BtnClick()
    {
        isClicked = !isClicked;
        if (isClicked) GameObject.Find("start_btn").GetComponent<Image>().sprite = sprites[0];
        else GameObject.Find("start_btn").GetComponent<Image>().sprite = sprites[1];
    }
    // Start is called before the first frame update

}
