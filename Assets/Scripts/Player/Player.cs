using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator anim;

    public GameManager gm;
    public ObjectManager om;
    //public Scrolling sc;

    public bool isHit;
    public bool isUp;
    public bool move;

    public float speed = 2.0f;
    public int life;
    public int score;

    private Vector3 targetPosition, previousPos, previousDirection;
    private Rigidbody2D rb2d;

    [HideInInspector]
    public bool isMoving;

    public float time;
    public bool Immortal;

    void Awake()
    {

    }

    void Start()
    {
        anim = GetComponent<Animator>();
        targetPosition = transform.position;
        previousDirection = new Vector3(0, 0, 0);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        ImmortalTime();
    }

    public void Move()
    {
        
        // Rotate the player by pressing left or right
        if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.LeftButton))
        {
            if (gm.gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (gm.nextLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                PlayerLeft();
            }
        }
        else if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.RightButton))
        {
            if (gm.gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (gm.nextLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                PlayerRight();
            }
        }

        // If we press the action button, move forward
        if (FigmentInput.GetButtonDown(FigmentInput.FigmentButton.ActionButton))
        {
            if (gm.gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (gm.nextLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                PlayerUp();
            }
        }

        Vector3 previousPos = transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -10.8f, 10.8f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

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
            previousDirection = direction;
        }
    }

    public void PlayerUp()
    {
        GameObject[] enemiesL = om.GetPool("EnemyL");
        GameObject[] enemiesM = om.GetPool("EnemyM");
        GameObject[] enemiesS = om.GetPool("EnemyS");
        GameObject[] enemiesBulletA = om.GetPool("BulletEnemyA");
        GameObject[] itemCoin = om.GetPool("ItemCoin");
        GameObject[] extraLife = om.GetPool("ExtraLife");

        Scrolling.scrolling();
        //sc.BGMove();

        if (!isUp)
        {
            if (!move)
            {
                isUp = true;
                move = true;
                anim.SetTrigger("up");
                Debug.Log("isUp");
            }
        }
        else
        {
            if (!move)
            {
                isUp = false;
                move = true;
                anim.SetTrigger("down");
                Debug.Log("isDown");
            }
        }

        move = false;

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
        for (int index = 0; index < enemiesBulletA.Length; index++)
        {
            if (enemiesBulletA[index].activeSelf)
            {
                Bullet bulletLogic = enemiesBulletA[index].GetComponent<Bullet>();
                bulletLogic.MoveDown();
            }
        }
        for (int index = 0; index < itemCoin.Length; index++)
        {
            if (itemCoin[index].activeSelf)
            {
                Item itemLogic = itemCoin[index].GetComponent<Item>();
                itemLogic.MoveDown();
            }
        }
        for (int index = 0; index < extraLife.Length; index++)
        {
            if (extraLife[index].activeSelf)
            {
                Item itemLogic = extraLife[index].GetComponent<Item>();
                itemLogic.MoveDown();
            }
        }
    }

    public void PlayerLeft()
    {
        targetPosition += Vector3.left;
        anim.SetFloat("xDir", -1);
        anim.SetTrigger("move");
    }

    public void PlayerRight()
    {
        targetPosition += Vector3.right;
        anim.SetFloat("xDir", 1);
        anim.SetTrigger("move");
    }

    public void ImmortalTime()
    {
        if (Immortal)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Abs(Mathf.Sin(Time.time * 7)));

            time += Time.deltaTime;
            //print(GetComponent<SpriteRenderer>().color);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void ImmortalExit()
    {
        Immortal = false;
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
                    Immortal = true;
                    Invoke("ImmortalExit", 2);

                    gm.OnHit();
                }

                collision.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 500;
                    gm.curScore = score;
                    break;
                case "ExtraLife":
                    if (life < 3)
                    {
                        life++;
                        gm.UpdateLifeIcon(life);
                    }
                    else
                    {
                        score += 1000;
                        gm.curScore = score;
                    }
                    break;
            }

            collision.gameObject.SetActive(false);
        }
    }
}
