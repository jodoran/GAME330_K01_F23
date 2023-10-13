using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public Transform[] ItemspawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public float maxItemDelay;
    public float curItemDelay;
    public int maxScore;
    public int curScore;
    public bool Stage1;
    public bool Stage2;
    public bool Stage3;
    public bool gameOver;
    public bool nextLevel;

    public GameObject player;
    public TMP_Text scoreText;
    public Image[] lifeImage;
    public ObjectManager objectManager;
    public GameObject GameOverPanel;
    public GameObject NextLevelPanel;

    void Awake()
    {
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };
        gameOver = false;
        nextLevel = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        curItemDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        if (curItemDelay > maxItemDelay)
        {
            SpawnItem();
            maxItemDelay = Random.Range(2.5f, 3f);
            curItemDelay = 0;
        }

        // UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

        CheckGameOver();
    }

    public void SpawnItem()
    {
        int ranItem = Random.Range(0, 10);
        if (ranItem < 6) //Not Item 50%
        {
            Debug.Log("Not Item");
        }
        else if (ranItem < 9) //Coin 40%
        {
            GameObject itemCoin = objectManager.MakeObj("ItemCoin");

            int ranPoint = Random.Range(0, 8);
            itemCoin.transform.position = ItemspawnPoints[ranPoint].position;

            Rigidbody2D rb2d = itemCoin.GetComponent<Rigidbody2D>();
            Item itemLogic = itemCoin.GetComponent<Item>();

            if (ranPoint == 0 || ranPoint == 1 || ranPoint == 2) // Right Spawn
            {
                rb2d.velocity = new Vector2(itemLogic.speed * (-1), 0);
            }
            else if (ranPoint == 3 || ranPoint == 4 || ranPoint == 5) // Left Spawn
            {
                rb2d.velocity = new Vector2(itemLogic.speed, 0);
            }
            else if (ranPoint == 6 || ranPoint == 7 || ranPoint == 8) // Up Spawn
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
        else if (ranItem < 10) //Coin 10%
        {
            GameObject itemLife = objectManager.MakeObj("ExtraLife");

            int ranPoint = Random.Range(0, 8);
            itemLife.transform.position = ItemspawnPoints[ranPoint].position;

            Rigidbody2D rb2d = itemLife.GetComponent<Rigidbody2D>();
            Item itemLogic = itemLife.GetComponent<Item>();

            if (ranPoint == 0 || ranPoint == 1 || ranPoint == 2) // Right Spawn
            {
                rb2d.velocity = new Vector2(itemLogic.speed * (-1), 0);
            }
            else if (ranPoint == 3 || ranPoint == 4 || ranPoint == 5) // Left Spawn
            {
                rb2d.velocity = new Vector2(itemLogic.speed, 0);
            }
            else if (ranPoint == 6 || ranPoint == 7 || ranPoint == 8) // Up Spawn
            {
                rb2d.velocity = new Vector2(0, 0);
            }
        }
    }

    public void STAGE1()
    {
        int ranEnemy = Random.Range(0, 1);
        int ranPoint = Random.Range(0, 6);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (ranPoint == 0 || ranPoint == 1 || ranPoint == 2) // Right Spawn
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rb2d.velocity = new Vector2(enemyLogic.speed * (-1), 0);
        }
        else if (ranPoint == 3 || ranPoint == 4 || ranPoint == 5) // Left Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rb2d.velocity = new Vector2(enemyLogic.speed, 0); //Shoot
        }
    }

    public void STAGE2()
    {
        int ranEnemy = Random.Range(0, 2);
        int ranPoint = Random.Range(0, 6);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (ranPoint == 0 || ranPoint == 1 || ranPoint == 2) // Right Spawn
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rb2d.velocity = new Vector2(enemyLogic.speed * (-1), 0);
        }
        else if (ranPoint == 3 || ranPoint == 4 || ranPoint == 5) // Left Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rb2d.velocity = new Vector2(enemyLogic.speed, 0); //Shoot
        }
    }

    public void STAGE3()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (ranPoint == 0 || ranPoint == 1 || ranPoint == 2) // Right Spawn
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rb2d.velocity = new Vector2(enemyLogic.speed * (-1), 0);
        }
        else if (ranPoint == 3 || ranPoint == 4 || ranPoint == 5) // Left Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rb2d.velocity = new Vector2(enemyLogic.speed, 0); //Shoot
        }
        else if (ranPoint == 6 || ranPoint == 7 || ranPoint == 8) // Up Spawn
        {
            enemy.transform.Rotate(Vector3.forward);
            rb2d.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
    }


    void SpawnEnemy()
    {
        if (Stage1)
        {
            STAGE1();
        }
        else if (Stage2)
        {
            STAGE2();
        }
        else
        {
            STAGE3();
        }
    }

    public void UpdateLifeIcon(int life)
    {
        //#1.UI Life Init Disable
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        //#2.UI Life Active
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void OnHit()
    {
        player.gameObject.layer = 6;
        Invoke("RespawnPlayer", 2);
    }

    public void RespawnPlayer()
    {
        player.gameObject.layer = 3;
        Debug.Log("Immortal Time Over");
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    public void CheckGameOver()
    {
        if (curScore >= maxScore)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (curScore >= maxScore)
        {
            Time.timeScale = 0;
            NextLevelPanel.SetActive(true);
            nextLevel = true;
            print("You Win");
        }
        else
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            gameOver = true;
            print("You Lose");
        }
    }
}
