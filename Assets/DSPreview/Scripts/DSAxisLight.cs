using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DSAxisLight : MonoBehaviour
{
    public string AxisName = "Vertical";
    public bool CheckPositive = true;
    public float DeadZoneValue = 0.1f;
    Image TargetImage;

    // Start is called before the first frame update
    void Start()
    {
        TargetImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float axisValue = Input.GetAxis(AxisName);
        bool overThreshold = false;
        if(CheckPositive)
        {
            overThreshold = axisValue > DeadZoneValue;
        }
        else
        {
            overThreshold = axisValue < -DeadZoneValue;
        }

        TargetImage.enabled = overThreshold;
    }
}