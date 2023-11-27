using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MYType.Unit Type;
    public UnitTeam unitTeam;
    public float Speed;
    public int damageAmount;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (unitTeam == UnitTeam.Blue)
        {
            rb.velocity = transform.right * Speed;
        }
        else if (unitTeam == UnitTeam.Red)
        {
            rb.velocity = -transform.right * Speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hits a unit with a different team
        UnitController unitController = collision.gameObject.GetComponent<UnitController>();
        if (unitController != null && unitController.unitTeam != unitTeam)
        {
            // Deal damage to the hit unit
            unitController.TakeDamage(damageAmount);

            // Destroy the projectile upon hitting a target
            Destroy(gameObject);
        }
    }
}
