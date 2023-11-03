using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DSButtonLight : MonoBehaviour
{
    public string InputName = "Fire1";
    Image TargetImage;

    // Start is called before the first frame update
    void Start()
    {
        TargetImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetImage.enabled = Input.GetButton(InputName);
    }
}
