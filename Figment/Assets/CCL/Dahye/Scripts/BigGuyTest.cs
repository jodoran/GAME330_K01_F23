using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Dev_Unit
{
    public class BigGuyTest : MonoBehaviour
    {
        [SerializeField] EnemySO enemySO;
        enum EnemyState
        {
            Idle,  
            Move,  
            Attack, 
            Dead
        }

        //Enemy Info
        private EnemyState enemyCurrentState; // = idle
        private float hp;
        //private float moveSpeed = 2f;
        private float damageAmount;
        private float idleTime;
        private bool isStop = false;
        private bool isDead = false;

        //Attack
        private float attackCoolTime = 0;
        private float rotationSpeed = 3.0f;
        private float stopDistance; //where enemy stop and start attack
        private GameObject playerTarget;



        private Animator enemyAnim;

        //Default Enemy Creation
        /*
        public void CreateEnemy(float moveSpeed, float hp, float damage, Vector3 position) 
        {
            //This Enemy Script = Scriptable Object Info
            //this.moveSpeed = moveSpeed;
            this.hp = hp;
            this.damageAmount = damage;
            this.isDead = false;
            this.gameObject.SetActive(true);
            this.transform.position = position;

            //생성 직후 State = Idle로 지정 
            //[ToDo] 어디에 생성돼?
        }*/

        //Dahye's Personal Note
        /* [완료] 에너미 상태 종류 
         * [완료] 필요한 준비물 변수 세팅
         * [완료] 에너미 생성
         * 어떤 상황에 어떤상태에 진입하는가? 
         * 상태에 진입했을 때 어떤행동을 하는가? 
         * 예시) 생성 후 Idle -> Idle의 기본정보 수치 지정 -> Idle 행동끝났다면 Move
         * 예시) Idle 행동 뒤 Move -> Move의 기본정보 지정 -> 앞으로 이동 행동 -> 조건 만족 시 Attack
         */

        void Start()
        {
            //게임 시작 시 5초 뒤 CreateEnemy() 
            Debug.Log("Game Start");
        }
        void Update()
        {
            FindPlayerTag();
            ActionsInStates();
        }

        void ChangeState(EnemyState desiredState)
        {
            if (enemyCurrentState == desiredState) return;
            Debug.Log("State change" + enemyCurrentState + "->" + desiredState);
            enemyCurrentState = desiredState;

            switch(enemyCurrentState) //State Begin Mode
            {
                case EnemyState.Idle:
                    {
                        Debug.Log("Idle State 진입!");
                        idleTime = 0; 
                        stopDistance = 0.5f;
                        isStop = true;
                        break;

                    }
                case EnemyState.Move:
                    {
                        Debug.Log("Move State 진입!");
                        stopDistance = 1.0f; //move until reach here
                        isStop = false;  
                        break;

                    }
                case EnemyState.Attack:
                    {
                        Debug.Log("Attack State 진입!");
                        break;

                    }
                case EnemyState.Dead:
                    {
                        Debug.Log("I'm Dead");
                        break;

                    }
            }
        }
        void ActionsInStates()
        {
            switch(enemyCurrentState)
            {
                case EnemyState.Idle:
                    {
                        Debug.Log("Idle State will change it to move soon");
                        idleTime += Time.deltaTime;
                        if (idleTime > 2.0f)
                            ChangeState(EnemyState.Move);
                        break;

                    }
                case EnemyState.Move:
                    {
                        //Move Forward
                       Vector3 newPos = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * enemySO.moveSpeed);
                       this.transform.position = newPos;  
                        Debug.Log("전진 완료");

                        //Move Until
                        if (playerTarget == null)
                        {
                            Debug.Log("타켓 미 감지");
                            break;
                        }
                    
                        float distance = Vector3.Distance(transform.position, playerTarget.transform.position);
                        Debug.Log("플레이어 현재위치 : " + playerTarget.transform.position + "에너미와의 거리 : " + distance);

                        if (distance < stopDistance) //특정거리도달
                        {
                            ChangeState(EnemyState.Attack);
                        }
                        break;
                    }
                case EnemyState.Attack:
                    {
                        break;

                    }
                case EnemyState.Dead:
                    {
                        break;
                    }
            }

        }
        //void SetEnemy()
            //Unit manager 혹은 big guy.cs 
        void FindPlayerTag()
        {
            playerTarget = null;

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (obj != null && obj.CompareTag("Player"))
                {
                    playerTarget = obj; //data type : game object
                    return;
                }
            }

            Debug.Log("Fail to find Player tag");
        }

        /* 만약 단순 돌진 후 성벽HP 깎이는 식이라면 상태변경 없이 HP깎는 상호작용
         * 만약 단순 돌진 후 explosion 과함께 사라지는 형식이라면 에너미 체력 높이고 충돌 시 이펙트 + 제거
         */
        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Wall"))
            {
                ChangeState(EnemyState.Attack);
            }
        }

    }

}