using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Unit
{
    public class ShootingGuyTest : EnemyBase
    {
        protected override void Awake()
        {
           base.Awake();
        }

        protected override void Start()
        {
            base.Start(); //Enemy Base의 Start 함수

           
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
    
        protected override void TriggerEneterAbstract(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                ChangeState(EnemyState.Attack);
            }
        }
    }

}