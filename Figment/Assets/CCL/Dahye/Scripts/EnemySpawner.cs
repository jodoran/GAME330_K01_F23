using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Dev_Unit
{
    
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySO enemyData; 
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private float spawnRate = 5f;
        [SerializeField] private float spawnTime = 5f;

        private float timer;

        EnemySO EnemyType1;
        EnemySO EnemyType2;
        EnemySO EnemyType3;

        private void Start()
        {
            EnemyType1 = UnitManager.Instance.GetEnemySO(EnemyType.BigGuy);
            EnemyType2 = UnitManager.Instance.GetEnemySO(EnemyType.SmallGuy);
            EnemyType3 = UnitManager.Instance.GetEnemySO(EnemyType.ShootingGuy);

            //SO ���� �����ٰ� �� �� (casting) ����ȭ ��

            //currEnemySO. = 10;
            timer = 0;
        }

        private void Update()
        {
            SpawnEnemy();
        }

        void SpawnEnemy()
        {
            
            timer += Time.deltaTime;
            int rand = Random.Range(0, spawnPoint.Length);
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            if (timer > spawnTime)
            {
                Instantiate(EnemyType1.enemyPrefab, spawnPoint[rand].position, rotation);
                timer = 0;
            }
        }

       



        //wave : enemy count ���� �� ��� hp = 0 �Ѵٸ� ���� ���̺� -> ���λ���
        //���ʹ� �ı��� setActive false
    }

}