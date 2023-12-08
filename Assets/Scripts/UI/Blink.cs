using System.Collections;
using TMPro;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float blinkSpeed;

    private void Start()
    {
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            yield return null;
        }
    }
}
