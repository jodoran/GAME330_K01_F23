using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dev_Unit : ���� ���� ����� �����ϴ� ���ӽ����̽�
namespace Dev_Unit
{
    // UnitManager Ŭ����: ������ ���� �� ������ ���
    public class UnitManager : MonoBehaviour
    {
        // �̱��� ������ ���� �ν��Ͻ� ����
        private static UnitManager instance;
        // �̱��� ������ �ν��Ͻ��� �ܺο��� ������ �� �ְ� �ϴ� ������Ƽ
        public static UnitManager Instance { get { return instance; } }


        // EnemySO �迭�� ��� Enemy Type���� SO�� ����
        [SerializeField] private EnemySO[] enemySO;

       
        private void Awake()
        {
            // �̹� �ν��Ͻ��� �������� �ʴ� ���
            if (instance == null)
            {
                // ���� ������ UnitManager�� ã��
                var manager = FindObjectOfType<UnitManager>();
                if (manager == null)
                {
                    // UnitManager�� ������ ���ο� ���� ������Ʈ�� �����ϰ� UnitManager ������Ʈ�� �߰�
                    GameObject obj = new GameObject("#UnitManager");
                    manager = obj.AddComponent<UnitManager>();
                }
                // �ν��Ͻ� ������ ã�Ƴ� manager�� �Ҵ�
                instance = manager;
                // �� ��ȯ �� �ı����� �ʵ��� ����
                DontDestroyOnLoad(instance);
            }
        }

        // �־��� EnemyType�� �ش��ϴ� EnemySO�� ��ȯ�ϴ� �޼���
        public EnemySO GetEnemySO(EnemyType enemyType)
        {
            // enemySO �迭���� enemyType�� ��ġ�ϴ� ù ��° EnemySO�� ã�� ��ȯ
            EnemySO enemy = System.Array.Find(enemySO, x => x.enemyType == enemyType);
            return enemy;
        }
    }
}