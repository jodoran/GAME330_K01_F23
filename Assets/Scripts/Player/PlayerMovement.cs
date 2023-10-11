using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    //public static Action player;

    Animator anim;

    bool dead, won = false;

    public float speed = 2.0f;

    private Vector3 pos, previousPos, previousDirection;
    private Rigidbody2D rb2d;

    [HideInInspector]
    public bool isMoving;

    void Awake()
    {
        //player = () => 
        //{ 
        //    Move();
        //    //ButtonDown(); 
        //};
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        pos = transform.position;
        previousDirection = new Vector3(0, 0, 0);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 previousPos = transform.position;

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

        Vector3 direction = transform.position - previousPos;
        direction = direction.normalized;

        if (direction == new Vector3(0, 0, 0))
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
            anim.SetFloat("xDir", direction.x);
            anim.SetFloat("yDir", direction.y);
            previousDirection = direction;
        }
    }

    public void ButtonDown(string type)
    {

        switch (type)
        {
            case "U":
                pos += Vector3.up;
                //anim.SetTrigger("isUp");
                anim.SetFloat("inputXDir", 0);
                anim.SetFloat("inputYDir", 1);
                anim.SetTrigger("move");
                Debug.Log("UP");
                break;
            case "L":
                pos += Vector3.left;
                //anim.SetTrigger("isLeft");
                anim.SetFloat("inputXDir", -1);
                anim.SetFloat("inputYDir", 0);
                anim.SetTrigger("move");
                Debug.Log("Left");
                break;
            case "R":
                pos += Vector3.right;
                //anim.SetTrigger("isRight");
                anim.SetFloat("inputXDir", 1);
                anim.SetFloat("inputYDir", 0);
                anim.SetTrigger("move");
                Debug.Log("Right");
                break;
        }
    }
}
