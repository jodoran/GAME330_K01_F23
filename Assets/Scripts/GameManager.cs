using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public bool GamePause;

    public GameObject player;
    public TMP_Text scoreText;
    public Image[] lifeImage;
    public ObjectManager objectManager;

    void Awake()
    {
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL" };
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        // UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 6);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;

        Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (!GamePause)
        {
            if (ranPoint == 0 || ranPoint == 1 || ranPoint == 2) // Right Spawn

            {
                enemy.transform.Rotate(Vector3.back * 90);
                rb2d.velocity = new Vector2(enemyLogic.speed * (-1), 0);

            }
            else if (ranPoint == 3 || ranPoint == 4 || ranPoint == 5) // Left Spawn

            {
                enemy.transform.Rotate(Vector3.forward * 90);
                rb2d.velocity = new Vector2(enemyLogic.speed, 0);
            }
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

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

}
