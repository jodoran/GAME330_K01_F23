using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BigGuyTest : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Dead
    }

    //Enemy Info
    private EnemyState enemyCurrentState;
    private float hp;
    private float moveSpeed;
    private float damageAmount;
    private float idleTime;
    private bool isStop = false;
    private bool isDead = false;

    //Attack
    private float attackCoolTime = 0;
    private float rotationSpeed = 3.0f;
    private float stopDistance; //where enemy stop and start attack
    private Transform playerTarget;


    private Animator enemyAnim;

    //Default Enemy Creation
    public void CreateEnemy(float moveSpeed, float hp, float damage, Vector3 Position) 
    {
        //This Enemy Script = Scriptable Object Info
        this.moveSpeed = moveSpeed;
        this.hp = hp;
        this.damageAmount = damage;
        this.isDead = false;
        this.gameObject.SetActive(true);
        this.transform.position = Position;

        //생성 직후 State = Idle로 지정 
        //[ToDo] 어디에 생성돼?
    }

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
    }
    void Update()
    {
        
    }

    void DefaultStateMachine(EnemyState desiredState)
    {
        if (enemyCurrentState == desiredState) return;
        enemyCurrentState = desiredState;

        switch(enemyCurrentState) //State Begin Mode
        {
            case EnemyState.Idle:
                {
                    idleTime = 0; 
                    stopDistance = 0.5f;
                    isStop = true;
                    break;

                }
            case EnemyState.Move:
                {
                    stopDistance = 1.0f; //move until reach here
                    isStop = false;  
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
    void ActionsInStates()
    {
        switch(enemyCurrentState)
        {
            case EnemyState.Idle:
                {
                    idleTime += Time.deltaTime;
                    if (idleTime > 2.0f)
                        DefaultStateMachine(EnemyState.Move);
                    break;

                }
            case EnemyState.Move:
                {
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

}
