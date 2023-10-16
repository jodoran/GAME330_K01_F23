using System.Collections.Generic;
using UnityEngine;

// UnitManager 클래스: 유닛의 생성 및 관리를 담당
public class UnitManager : MonoBehaviour
{
    /// <summary>
    /// 최종 점수
    /// </summary>
    public int Score { get { return score; } }
    private int score = 0;

    /// <summary>
    /// 유닛 추가
    /// </summary>
    /// <param name="enemy"></param>
    public void AddUnit(Enemy_DH enemy)
    {
        enemyList.Add(enemy);
    }

    /// <summary>
    /// 유닛 제거
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
    /// 에너미 존재여부 체크
    /// </summary>
    /// <returns></returns>
    public bool IsEnemyAllDie()
    {
        return enemyList.Count <= 0;
    }

    private List<Enemy_DH> enemyList = new List<Enemy_DH>();

    // 싱글턴 패턴을 위한 인스턴스 변수
    private static UnitManager instance;
    // 싱글턴 패턴의 인스턴스를 외부에서 접근할 수 있게 하는 프로퍼티
    public static UnitManager Instance { get { return instance; } }

    private void Awake()
    {
        // 이미 인스턴스가 존재하지 않는 경우
        if (instance == null)
        {
            // 현재 씬에서 UnitManager를 찾음
            var manager = FindObjectOfType<UnitManager>();
            if (manager == null)
            {
                // UnitManager가 없으면 새로운 게임 오브젝트를 생성하고 UnitManager 컴포넌트를 추가
                GameObject obj = new GameObject("#UnitManager");
                manager = obj.AddComponent<UnitManager>();
            }
            // 인스턴스 변수에 찾아낸 manager를 할당
            instance = manager;
            // 씬 전환 시 파괴되지 않도록 설정
            DontDestroyOnLoad(instance);
        }
    }
}
