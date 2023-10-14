using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dev_Unit
{
    public abstract class EnemyBase : MonoBehaviour
    {
        protected enum EnemyState
        {
            Idle,
            Move,
            Attack,
            Dead
        }

        //Enemy Info
        //Alt + Shift + <- 방향키 
        protected EnemyState enemyCurrentState; // = idle
        protected float hp;
        protected float moveSpeed = 2f;
        protected float damageAmount;
        protected float idleTime;
        protected bool isStop = false;
        protected bool isDead = false;

        //Attack
        protected float attackCoolTime = 0;
        protected float stopDistance; //where enemy stop and start attack
        protected Transform playerTarget;

        //Animator
        protected Animator enemyAnim;

        //Event
        public UnityAction OnDead;


        //Enemy 수치값 초기셋팅
        public void enemySetting(EnemySO enemySO)
        {
            //Unit Manager Setting 
            UnitManager.Instance.AddUnit(this);

            this.moveSpeed = enemySO.moveSpeed;
            this.hp = enemySO.hp;
            this.damageAmount = enemySO.damage;
            this.isDead = false;
            this.gameObject.SetActive(true);
        }


        protected virtual void Awake()
        {
            enemyAnim = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            //플레이어 찾기
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        }

        // Update is called once per frame
        // Update is called once per frame
        protected virtual void Update()
        {

        }

        public void OnDamaged(float damage)
        {
            this.hp -= damage;
            //hp
            //damageAmount
        }



        void OnTriggerEnter(Collider other)
        {
            TriggerEneterAbstract(other);
        }

        protected abstract void TriggerEneterAbstract(Collider other);

        



        private void OnDisable()
        {
            UnitManager.Instance.RemoveUnit(this);
        }


    }

}
//자동 줄맞춤 ctrl K D