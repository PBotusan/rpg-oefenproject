using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{
    /// <summary>
    /// Spawn particle effect for attack.
    /// </summary>
    [SerializeField] GameObject groundImpact_Spawn, kickFX_Spawn, fireTornado_Spawn, fireShield_Spawn, heal_Spawn;

    /// <summary>
    /// Prefab Attack particle for attack.
    /// </summary>
    [SerializeField] GameObject groundImpact_Prefab, kickFX_Prefab, fireTornado_Prefab, fireShield_Prefab, healFX_prefab, thunderFX_Prefab;


    private void GroundImpact()
    {
        Instantiate(groundImpact_Prefab, groundImpact_Spawn.transform.position, Quaternion.identity);
    }

    private void Kick()
    {
        Instantiate(kickFX_Prefab, kickFX_Spawn.transform.position, Quaternion.identity);
    }

    private void FireTornado()
    {
        Instantiate(fireTornado_Prefab, fireTornado_Spawn.transform.position, Quaternion.identity);
    }

    private void FireShield()
    {
         GameObject fireObj = Instantiate(fireShield_Prefab, fireShield_Spawn.transform.position, Quaternion.identity) as GameObject;
        fireObj.transform.SetParent(transform);
    }

    private void Heal()
    {
        Vector3 temp = transform.position;
        temp.y += 2f;

        GameObject healObj = Instantiate(healFX_prefab, heal_Spawn.transform.position, Quaternion.identity) as GameObject;
        healObj.transform.SetParent(transform);
    }

    private void ThunderAttack()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 position = Vector3.zero;

          
            if (i == 0)
            {
                position = new Vector3(transform.position.x - 4f, transform.position.y + 2f, 
                    transform.position.z);
            }
            else if(i == 1)
            {
                position = new Vector3(transform.position.x + 4f, transform.position.y + 2f, 
                    transform.position.z);
            }
            else if (i == 2)
            {
                position = new Vector3(transform.position.x, transform.position.y + 2f, 
                    transform.position.z - 2f);
            }
            else if (i == 3)
            {
                position = new Vector3(transform.position.x, transform.position.y + 2f, 
                    transform.position.z + 2f);
            }
            else if (i == 4)
            {
                position = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, 
                    transform.position.z + 2.5f);
            }
            else if (i == 5)
            {
                position = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f,
                    transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                position = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f,
                    transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                position = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f,
                    transform.position.z + 2.5f);
            }

            Instantiate(thunderFX_Prefab, position, Quaternion.identity);
        }
        
    }

  
}
