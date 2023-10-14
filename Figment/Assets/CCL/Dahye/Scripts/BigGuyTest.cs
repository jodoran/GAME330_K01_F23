using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class BigGuyTest : EnemyBase
    {
       
        protected override void Awake()
        {
           base.Awake();
            
        }

        protected override void Start()
        {
            base.Start(); //Enemy Base의 Start 함수

            //게임 시작 시 5초 뒤 CreateEnemy() 
            Debug.Log("Game Start");
        }
        protected override void Update()
        {
            base.Update();
            ActionsInStates();
        }

        void ChangeState(EnemyState desiredState)
        {
            if (enemyCurrentState == desiredState) return;
            Debug.Log("State change" + enemyCurrentState + "->" + desiredState);
            enemyCurrentState = desiredState;

            switch (enemyCurrentState) //State Begin Mode
            {
                case EnemyState.Idle:
                    {
                        Debug.Log("Idle State 진입!");
                        base.idleTime = 0;
                        base.stopDistance = 0.5f;
                        base.isStop = true;
                        break;

                    }
                case EnemyState.Move:
                    {
                        Debug.Log("Move State 진입!");
                        base.stopDistance = 1.0f; //move until reach here
                        base.isStop = false;
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
                        base.OnDead?.Invoke(); //이벤트 발동 //? = null 체크 
                        base.OnDead = null; // 이벤트 사용 시 항상 초기화
                        break;

                    }
            }
        }
        void ActionsInStates()
        {
            switch (base.enemyCurrentState)
            {
                case EnemyState.Idle:
                    {
                        base.idleTime += Time.deltaTime;
                        if (base.idleTime > 2.0f)
                            ChangeState(EnemyState.Move);
                        break;

                    }
                case EnemyState.Move:
                    {
                        //Move Forward
                        Vector3 newPos = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * base.moveSpeed);
                        this.transform.position = newPos;

                        //Move Until
                        if (base.playerTarget == null)
                        {
                            Debug.Log("타켓 미 감지");
                            break;
                        }

                        float distance = Vector3.Distance(transform.position, base.playerTarget.transform.position);

                        if (distance < base.stopDistance) //특정거리도달
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


        /* 만약 단순 돌진 후 성벽HP 깎이는 식이라면 상태변경 없이 HP깎는 상호작용
         * 만약 단순 돌진 후 explosion 과함께 사라지는 형식이라면 에너미 체력 높이고 충돌 시 이펙트 + 제거
         */
        protected override void TriggerEneterAbstract(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                ChangeState(EnemyState.Attack);
            }
        }
    }

}