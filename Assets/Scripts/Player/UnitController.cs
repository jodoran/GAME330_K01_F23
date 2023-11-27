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

    [Header("==========Type=========")]
    public MYType.Unit playerType;
    public UnitTeam unitTeam;

    public GameObject Dust;

    [Header("==========Unit=========")]
    public int HP;
    public int Atk;
    public float moveSpeed;
    public float detectionDistance;
    public float minAtkRange;
    public float maxAtkRange;

    private bool battleState;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetTrigger("doMove");
        Dust.SetActive(true);
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
        }
        else
        {
            Dust.SetActive(false);
        }
    }

    public void Detact()
    {
        //#.Raycast 방향 설정
        Vector2 raycastDirection = (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left;

        //#.현재 방향으로만 Raycast를 쏴서 적군을 감지하는 로직
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, detectionDistance);

        //#.Ray를 시각적으로 그리기
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
                    //Debug.Log("빨간 적군을 감지했습니다! 공격을 시작합니다.");
                    //StartCoroutine(nameof(Attack));
                    switch (playerType)
                    {
                        case MYType.Unit.Sword:
                            StartCoroutine(nameof(Attack));
                            break;
                        case MYType.Unit.Guard:
                            StartCoroutine(nameof(Attack));
                            break;
                        case MYType.Unit.Range:
                            StartCoroutine(nameof(Shoot));
                            break;
                        case MYType.Unit.Wizard:
                            StartCoroutine(nameof(Shoot));
                            break;
                    }
                }
                else if (enemyUnit.unitTeam == UnitTeam.Red)
                {
                    //Debug.Log("파란 적군을 감지했습니다! 공격을 시작합니다.");
                    //StartCoroutine(nameof(Attack));
                    switch (playerType)
                    {
                        case MYType.Unit.Sword:
                            StartCoroutine(nameof(Attack));
                            break;
                        case MYType.Unit.Guard:
                            StartCoroutine(nameof(Attack));
                            break;
                        case MYType.Unit.Range:
                            StartCoroutine(nameof(Shoot));
                            break;
                        case MYType.Unit.Wizard:
                            StartCoroutine(nameof(Shoot));
                            break;
                    }
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

    void FireProjectile(string projectilePrefabName)
    {
        // Instantiate the projectile prefab
        GameObject projectilePrefab = Resources.Load<GameObject>(projectilePrefabName);

        if (projectilePrefab != null)
        {
            Vector3 curPos = new Vector3(transform.position.x, transform.position.y + 8, transform.position.z);
            GameObject projectile = Instantiate(projectilePrefab, curPos, Quaternion.identity);

            // Set the projectile's team to match the unit's team
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null)
            {
                projectileScript.unitTeam = unitTeam;
            }
        }
    }

    IEnumerator Attack()
    {
        // Check if the unit is defeated
        if (HP <= 0)
        {
            // Handle defeat logic, e.g., play death animation, destroy object, etc.
            isAttacking = true;
            animator.SetTrigger("doDie");
            StartCoroutine(DestroyAfterDelay(1.0f));
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
                    float randomWaitTime = Random.Range(minAtkRange, maxAtkRange);
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

    IEnumerator Shoot()
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
                    switch (playerType)
                    {
                        case MYType.Unit.Range:
                            if (unitTeam == UnitTeam.Blue)
                            {
                                FireProjectile("ArrowBlue");
                            }
                            else if (unitTeam == UnitTeam.Red)
                            {
                                FireProjectile("ArrowRed");
                            }
                            break;
                        case MYType.Unit.Wizard:
                            if (unitTeam == UnitTeam.Blue)
                            {
                                FireProjectile("MagicBlue");
                            }
                            else if (unitTeam == UnitTeam.Red)
                            {
                                FireProjectile("MagicRed");
                            }
                            break;
                    }

                    // Wait for some time before returning to move state
                    float randomWaitTime = Random.Range(minAtkRange, maxAtkRange);
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
}
