using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    /// <summary>
    /// Enemy Health amount
    /// </summary>
    [SerializeField] float health = 100f;


    /// <summary>
    /// Used To damage enemy
    /// </summary>
    /// <param name="damageAmount"></param>
    internal void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy Took Damage, health is " + health);

        if (health < 0)
        {
            //enemy is dead

        }

    }


}
