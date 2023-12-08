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
    public LayerMask targetMask;

    public GameObject Dust;

    [Header("==========Unit=========")]
    public int unitCost;
    public int HP;
    public int Atk;
    public float moveSpeed;
    public float detectionDistance;
    public float minAtkRange;
    public float maxAtkRange;

    public bool battleState;
    private bool isAttacking = false;

    [Header("==========Audio=========")]
    public AudioClip[] clip;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if(playerType != MYType.Unit.Castle)
        {
            animator.SetTrigger("doMove");
            Move();
            Detact();
        }
    }

    public void Update()
    {
        Move();
        Detact();
    }

    public void Move()
    {
        if (playerType == MYType.Unit.Castle)
            return;

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

        if (HP <= 0)
        {
            // Handle defeat logic, e.g., play death animation, destroy object, etc.
            isAttacking = true;
            animator.SetTrigger("doDie");
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
    }

    public void Detact()
    {
        if (playerType == MYType.Unit.Castle)
            return;

        //#.Raycast 방향 설정
        Vector2 raycastDirection = (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left;

        //#.현재 방향으로만 Raycast를 쏴서 적군을 감지하는 로직
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDirection, detectionDistance, targetMask);

        //#.Ray를 시각적으로 그리기
        Debug.DrawRay(transform.position, raycastDirection * detectionDistance, Color.green);

        UnitController nearestEnemy = null;
        foreach (var hitItem in hit)
        {
            if (hitItem.collider != null)
            {
                UnitController enemyUnit = hitItem.collider.gameObject.GetComponent<UnitController>();
                //battleState = false;

                if (enemyUnit != null && enemyUnit.unitTeam != unitTeam)
                {
                    if (nearestEnemy == null)
                    {
                        nearestEnemy = enemyUnit;
                        //nearestEnemy.GetComponent<SpriteRenderer>().color  = Color.blue;
                    }
                    else if (Vector3.Distance(transform.position, enemyUnit.transform.position) <
                            Vector3.Distance(transform.position, nearestEnemy.transform.position))
                    {
                        nearestEnemy = enemyUnit;
                    }
                }
                else
                {
                    continue;
                }
            }
        }

        if (nearestEnemy != null)
        {
            battleState = true;

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
                    //print("Distance : " + detectionDistance + " " + Vector2.Distance(transform.position, nearestEnemy.transform.position));
                    break;
                case MYType.Unit.Wizard:
                    StartCoroutine(nameof(Shoot));
                    //print("Distance : " + detectionDistance + " " + Vector2.Distance(transform.position, nearestEnemy.transform.position));
                    break;
            }

            if (HP <= 0)
            {
                return;
            }
        }
        else
        {
            battleState = false;
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
        if (playerType == MYType.Unit.Castle)
        {
            animator.SetFloat("HP", HP);
            SoundManager.instance.SFXPlay("Castle", clip[0]);
        }

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
            Vector3 curPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
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
            StartCoroutine(DestroyAfterDelay(0.5f));
        }

        if (!isAttacking)
        {
            // Set the attacking flag to true
            isAttacking = true;

            switch (playerType)
            {
                case MYType.Unit.Sword:
                    SoundManager.instance.SFXPlay("Sword", clip[0]);
                    break;
                case MYType.Unit.Guard:
                    SoundManager.instance.SFXPlay("Shield", clip[0]);
                    break;
            }

            Vector2 raycastDirection = (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left;

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDirection, detectionDistance, targetMask);

            UnitController nearestEnemy = null;
            foreach (var hitItem in hit)
            {
                if (hitItem.collider != null)
                {
                    UnitController enemyUnit = hitItem.collider.gameObject.GetComponent<UnitController>();
                    //battleState = false;

                    if (enemyUnit != null && enemyUnit.unitTeam != unitTeam)
                    {
                        if (nearestEnemy == null)
                        {
                            nearestEnemy = enemyUnit;
                            //nearestEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                        else if (Vector3.Distance(transform.position, enemyUnit.transform.position) <
                                Vector3.Distance(transform.position, nearestEnemy.transform.position))
                        {
                            nearestEnemy = enemyUnit;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                UnitController enemyUnit = nearestEnemy;

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
            //battleState = false;
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
            StartCoroutine(DestroyAfterDelay(0.5f));
        }

        if (!isAttacking)
        {
            // Set the attacking flag to true
            isAttacking = true;

            switch (playerType)
            {
                case MYType.Unit.Range:
                    SoundManager.instance.SFXPlay("Arrow", clip[0]);
                    break;
                case MYType.Unit.Wizard:
                    SoundManager.instance.SFXPlay("Magic", clip[0]);
                    break;
            }

            // Assuming there is an enemy unit detected
            //RaycastHit2D hit = Physics2D.Raycast(transform.position,
            //                                     (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left,
            //                                     detectionDistance);

            Vector2 raycastDirection = (unitTeam == UnitTeam.Blue) ? Vector2.right : Vector2.left;

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDirection, detectionDistance, targetMask);

            UnitController nearestEnemy = null;
            foreach (var hitItem in hit)
            {
                if (hitItem.collider != null)
                {
                    UnitController enemyUnit = hitItem.collider.gameObject.GetComponent<UnitController>();
                    //battleState = false;

                    if (enemyUnit != null && enemyUnit.unitTeam != unitTeam)
                    {
                        if (nearestEnemy == null)
                        {
                            nearestEnemy = enemyUnit;
                            //nearestEnemy.GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                        else if (Vector3.Distance(transform.position, enemyUnit.transform.position) <
                                Vector3.Distance(transform.position, nearestEnemy.transform.position))
                        {
                            nearestEnemy = enemyUnit;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                UnitController enemyUnit = nearestEnemy;

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
            //battleState = false;
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        SoundManager.instance.SFXPlay("Die", clip[1]);
        // Destroy the game object
        Destroy(gameObject);
        StopAllCoroutines();
    }
}
