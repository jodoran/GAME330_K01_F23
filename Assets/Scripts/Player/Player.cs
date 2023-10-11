using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    Animator anim;

    public GameManager gm;
    public ObjectManager om;

    public bool isHit;
    bool dead, won = false;

    public float speed = 2.0f;
    public int life;
    public int score;

    private Vector3 pos, previousPos, previousDirection;
    private Rigidbody2D rb2d;

    [HideInInspector]
    public bool isMoving;

    void Awake()
    {

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
            anim.SetFloat("xDir", 0);
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
        GameObject[] enemiesL = om.GetPool("EnemyL");
        GameObject[] enemiesM = om.GetPool("EnemyM");
        GameObject[] enemiesS = om.GetPool("EnemyS");

        switch (type)
        {
            case "U":
                //pos += Vector3.up;
                anim.SetFloat("inputXDir", 0);
                anim.SetFloat("inputYDir", 1);
                anim.SetTrigger("move");

                for (int index = 0; index < enemiesL.Length; index++)
                {
                    if (enemiesL[index].activeSelf)
                    {
                        Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                        enemyLogic.MoveDown();
                    }
                }
                for (int index = 0; index < enemiesM.Length; index++)
                {
                    if (enemiesM[index].activeSelf)
                    {
                        Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                        enemyLogic.MoveDown();
                    }
                }
                for (int index = 0; index < enemiesS.Length; index++)
                {
                    if (enemiesS[index].activeSelf)
                    {
                        Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                        enemyLogic.MoveDown();
                    }
                }

                Debug.Log("UP");
                break;
            case "L":
                pos += Vector3.left;
                anim.SetFloat("inputXDir", -1);
                anim.SetFloat("inputYDir", 0);
                anim.SetTrigger("move");
                Debug.Log("Left");
                break;
            case "R":
                pos += Vector3.right;
                anim.SetFloat("inputXDir", 1);
                anim.SetFloat("inputYDir", 0);
                anim.SetTrigger("move");
                Debug.Log("Right");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
            {
                return;
            }
            else
            {
                life--;
                gm.UpdateLifeIcon(life);

                if (life <= 0)
                {
                    gm.GameOver();
                }
                else
                {
                    gm.RespawnPlayer();

                }

                gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                //case "Power":
                //    if (power == maxPower)
                //    {
                //        score += 500;
                //    }
                //    else
                //    {
                //        power++;
                //    }
                //    break;
                //case "Ulti":
                //    if (ulti == maxUlti)
                //    {
                //        score += 500;
                //    }
                //    else
                //    {
                //        ulti++;
                //        gm.UpdateUltiIcon(ulti);
                //    }
                //    break;
            }

            collision.gameObject.SetActive(false);
        }
    }
}
