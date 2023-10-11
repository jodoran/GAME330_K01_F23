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
    public GameObject bulletObjB;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemUlti;
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
                Health = 40;
                break;
            case "M":
                Health = 10;
                break;
            case "S":
                Health = 3;
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

        if (enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            //Instantiate(bulletObjA, transform.position, transform.rotation);

            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = Vector3.down;
            rb2d.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            //GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            //bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            //Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            //GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            //bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            //Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            //Rigidbody2D rb2dR = bulletR.GetComponent<Rigidbody2D>();
            //Rigidbody2D rb2dL = bulletL.GetComponent<Rigidbody2D>();

            //Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            //Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            //rb2dR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            //rb2dL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
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
                int ran = Random.Range(0, 10);
                if (ran < 3) //Not Item 30%
                {
                    Debug.Log("Not Item");
                }
                else if (ran < 10) //Coin 70%
                {
                    GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                    itemCoin.transform.position = transform.position;
                    //Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
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
        //else if (collision.gameObject.tag == "PlayerBullet")
        //{
        //    Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        //    OnHit(bullet.dmg);


        //    //gameObject.SetActive(false);
        //    //Destroy(collision.gameObject);
        //    collision.gameObject.SetActive(false);
        //}
    }

    public void MoveDown()
    {
        Vector3 curPos = transform.position;
        curPos += Vector3.down;
        transform.position = curPos;
    }
}
