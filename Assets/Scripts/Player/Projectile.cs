using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public MYType.Unit Type;
    public UnitTeam unitTeam;
    public float Speed;
    public bool isHit;

    public int damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("1");
        // Check if the projectile hits a unit with a different team
        UnitController unitController = collision.gameObject.GetComponent<UnitController>();
        if (unitController != null && unitController.unitTeam != unitTeam && !isHit)
        {
            print("2");
            // Deal damage to the hit unit
            unitController.TakeDamage(damageAmount);

            // Set isHit to true to prevent multiple damage on the same target
            isHit = true;

            // Optionally, destroy the projectile after hitting a target
            Destroy(gameObject);
        }
        else
        {
            print("3");
        }
    }
}
