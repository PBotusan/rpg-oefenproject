using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    /// <summary>
    /// Layer used for enemy
    /// </summary>
    [SerializeField] LayerMask enemyLayer;

    /// <summary>
    /// Radius used for attack
    /// </summary>
    [SerializeField] float radius = 0.5f;

    /// <summary>
    /// Amount of damage
    /// </summary>
    [SerializeField] float damageAmount = 10f;

    private EnemyHealth enemyHealth;
    private bool collided;


    // Update is called once per frame
    void Update()
    {
        CheckIfCollided();
        
    }


    /// <summary>
    /// Check if the hit is colliding with the enemy.
    /// </summary>
    private void CheckIfCollided()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider collider in hits)
        {
            enemyHealth = collider.gameObject.GetComponent<EnemyHealth>();
            collided = true;
        }

        if (collided)
        {
            enemyHealth.TakeDamage(damageAmount);
            enabled = false;
        }

    }
}
