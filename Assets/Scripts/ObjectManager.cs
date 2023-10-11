using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemUltiPrefab;
    public GameObject bulletEnemyAPrefab;

    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemUlti;

    GameObject[] bulletEnemyA;

    GameObject[] targetPool;

    void Awake()
    {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemUlti = new GameObject[10];

        bulletEnemyA = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        //#1.Enemy
        for (int index = 0; index < enemyL.Length; index++) //#.EnemyL
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }

        for (int index = 0; index < enemyM.Length; index++) //#.EnemyM
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }

        for (int index = 0; index < enemyS.Length; index++) //#.EnemyS
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        //#2.Item
        for (int index = 0; index < itemCoin.Length; index++) //#.ItemCoin
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }

        //for (int index = 0; index < itemPower.Length; index++) //#.ItemPower
        //{
        //    itemPower[index] = Instantiate(itemPowerPrefab);
        //    itemPower[index].SetActive(false);
        //}

        //for (int index = 0; index < itemUlti.Length; index++) //#.ItemUlti
        //{
        //    itemUlti[index] = Instantiate(itemUltiPrefab);
        //    itemUlti[index].SetActive(false);
        //}

        //#3.Bullet
        for (int index = 0; index < bulletEnemyA.Length; index++) //#.bulletEnemyA
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemUlti":
                targetPool = itemUlti;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemUlti":
                targetPool = itemUlti;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
        }

        return targetPool;
    }
}
