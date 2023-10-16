using System.Collections.Generic;
using UnityEngine;

// UnitManager Ŭ����: ������ ���� �� ������ ���
public class UnitManager : MonoBehaviour
{
    /// <summary>
    /// ���� ����
    /// </summary>
    public int Score { get { return score; } }
    private int score = 0;

    /// <summary>
    /// ���� �߰�
    /// </summary>
    /// <param name="enemy"></param>
    public void AddUnit(Enemy_DH enemy)
    {
        enemyList.Add(enemy);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveUnit(Enemy_DH enemy)
    {

        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            score += enemy.Score;
        }
    }

    /// <summary>
    /// ���ʹ� ���翩�� üũ
    /// </summary>
    /// <returns></returns>
    public bool IsEnemyAllDie()
    {
        return enemyList.Count <= 0;
    }

    private List<Enemy_DH> enemyList = new List<Enemy_DH>();

    // �̱��� ������ ���� �ν��Ͻ� ����
    private static UnitManager instance;
    // �̱��� ������ �ν��Ͻ��� �ܺο��� ������ �� �ְ� �ϴ� ������Ƽ
    public static UnitManager Instance { get { return instance; } }

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
}
