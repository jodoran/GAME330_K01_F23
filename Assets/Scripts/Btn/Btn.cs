using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Btn : MonoBehaviour
{
    Button button;

    private Player player;
    private EventTrigger et;

    void Start()
    {
        button = GetComponent<Button>();
        player = GameObject.FindObjectOfType<Player>();
        et = GetComponent<EventTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        button.interactable = !player.isMoving;
        et.enabled = button.interactable;
    }
}
