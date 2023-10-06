using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dev_Unit : 유닛 관련 기능을 포함하는 네임스페이스
namespace Dev_Unit
{
    // UnitManager 클래스: 유닛의 생성 및 관리를 담당
    public class UnitManager : MonoBehaviour
    {
        // 싱글턴 패턴을 위한 인스턴스 변수
        private static UnitManager instance;
        // 싱글턴 패턴의 인스턴스를 외부에서 접근할 수 있게 하는 프로퍼티
        public static UnitManager Instance { get { return instance; } }


        // EnemySO 배열로 모든 Enemy Type들의 SO를 받음
        [SerializeField] private EnemySO[] enemySO;

       
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

        // 주어진 EnemyType에 해당하는 EnemySO를 반환하는 메서드
        public EnemySO GetEnemySO(EnemyType enemyType)
        {
            // enemySO 배열에서 enemyType과 일치하는 첫 번째 EnemySO를 찾아 반환
            EnemySO enemy = System.Array.Find(enemySO, x => x.enemyType == enemyType);
            return enemy;
        }
    }
}