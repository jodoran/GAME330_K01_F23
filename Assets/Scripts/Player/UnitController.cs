using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum UnitTeam
{
    Blue,
    Red
}

public class UnitController : MonoBehaviour
{
    Animator animator;

    public MYType.Unit playerType;
    public UnitTeam unitTeam;

    public GameObject Dust;
    //public ParticleSystem Dust;
    public float moveSpeed;
    public float detectionDistance;

    [Header("==========Unit=========")]
    public int HP;
    public int Atk;
    private bool battleState;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        //Dust = GetComponent<ParticleSystem>();

        animator.SetTrigger("doMove");
        Dust.SetActive(true);
        //Dust.Play(Dust);
    }

    public void Update()
    {
        Move();
        Detact();
    }

    public void Move()
    {
        if (!battleState)
        {
            float moveDirection = (unitTeam == UnitTeam.Blue) ? 1f : -1f;
            transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);
            Dust.SetActive(true);
            //Dust.Play(Dust);
        }
        else
        {
            Dust.SetActive(false);
            //Dust.Stop(Dust);
        }
    }

    public void Detact()
    {
        //#.Raycast ���� ����
        Vector2 raycastDirection = (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left;

        //#.���� �������θ� Raycast�� ���� ������ �����ϴ� ����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, detectionDistance);

        //#.Ray�� �ð������� �׸���
        Debug.DrawRay(transform.position, raycastDirection * detectionDistance, Color.green);

        if (hit.collider != null)
        {
            UnitController enemyUnit = hit.collider.gameObject.GetComponent<UnitController>();
            battleState = false;

            if (enemyUnit != null && enemyUnit.unitTeam != unitTeam)
            {
                battleState = true;
                
                if (enemyUnit.unitTeam == UnitTeam.Blue)
                {
                    //Debug.Log("���� ������ �����߽��ϴ�! ������ �����մϴ�.");
                    StartCoroutine(nameof(Attack));
                }
                else if (enemyUnit.unitTeam == UnitTeam.Red)
                {
                    //Debug.Log("�Ķ� ������ �����߽��ϴ�! ������ �����մϴ�.");
                    StartCoroutine(nameof(Attack));
                }
                if (HP <= 0)
                {
                    return;
                }
            }
        }
    }

    public void AttackTarget(UnitController target)
    {
        // Apply damage to the target
        target.TakeDamage(Atk);

        // Add any additional logic for the attack
    }

    public void TakeDamage(int damage)
    {
        // Reduce HP by the damage amount
        HP -= damage;

        // Play hit animation
        animator.SetTrigger("doHit");
    }

    IEnumerator Attack()
    {
        // Check if the unit is defeated
        if (HP <= 0)
        {
            // Handle defeat logic, e.g., play death animation, destroy object, etc.
            isAttacking = true;
            animator.SetTrigger("doDie");
            StartCoroutine(DestroyAfterDelay(3.0f));
            yield break;
        }

        if (!isAttacking)
        {
            // Set the attacking flag to true
            isAttacking = true;

            // Assuming there is an enemy unit detected
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                 (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left,
                                                 detectionDistance);

            if (hit.collider != null)
            {
                UnitController enemyUnit = hit.collider.gameObject.GetComponent<UnitController>();

                if (enemyUnit != null && enemyUnit.unitTeam != unitTeam)
                {
                    // Attack the enemy
                    animator.SetTrigger("doAttack");
                    AttackTarget(enemyUnit);

                    // Wait for some time before returning to move state
                    float randomWaitTime = Random.Range(1.0f, 3.0f);
                    yield return new WaitForSeconds(randomWaitTime);
                }
            }

            // Reset the attacking flag to false
            isAttacking = false;

            // Reset to move state
            animator.SetTrigger("doMove");
            battleState = false;
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the game object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("1");
        // Check if the projectile hits a unit with a different team
        UnitController unitController = collision.gameObject.GetComponent<UnitController>();
        if (unitController != null && unitController.unitTeam != unitTeam)
        {
            print("2");
            // Deal damage to the hit unit
            unitController.TakeDamage(Atk);

            // Optionally, destroy the projectile after hitting a target
            Destroy(gameObject);
        }
        else
        {
            print("3");
        }
    }
}
