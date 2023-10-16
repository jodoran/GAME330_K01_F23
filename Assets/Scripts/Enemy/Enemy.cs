using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int Health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject player;

    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                Health = 30;
                break;
            case "M":
                Health = 10;
                break;
            case "S":
                Health = 10;
                break;
        }

    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay)
        {
            return;
        }

        if (enemyName == "M")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = Vector3.down * 5;
            rb2d.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }

        curShotDelay = 0.1F;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        if (Health <= 0)
        {
            return;
        }
        else
        {
            Health -= dmg;
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);

            if (Health <= 0)
            {
                Player playerLogic = player.GetComponent<Player>();
                playerLogic.score += enemyScore;

                //#.Random Ratio Item Drop
                int ran = Random.Range(0, 8);
                if (ran < 3) //Not Item 30%
                {
                    Debug.Log("Not Item");
                }
                else if (ran < 8) //Coin 70%
                {
                    GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                    itemCoin.transform.position = transform.position;
                    //Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
                }
                else
                {
                     if (ran < 10) //ExtraLife 20%
                    {
                        GameObject extraLife = objectManager.MakeObj("ExtraLife");
                        extraLife.transform.position = transform.position;
                    }
                }
                //else if (ran < 8) //Power 20%
                //{
                //    GameObject itemPower = objectManager.MakeObj("ItemPower");
                //    itemPower.transform.position = transform.position;
                //    //Instantiate(itemPower, transform.position, itemPower.transform.rotation);
                //}
                //else if (ran < 10) //Boom 20%
                //{
                //    GameObject itemUlti = objectManager.MakeObj("ItemUlti");
                //    itemUlti.transform.position = transform.position;
                //    //Instantiate(itemUlti, transform.position, itemUlti.transform.rotation);
                //}

                gameObject.SetActive(false);
                transform.rotation = Quaternion.identity;
            }
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    public void MoveDown()
    {
        Vector3 curPos = transform.position;
        curPos += Vector3.down;
        transform.position = curPos;
    }
}
