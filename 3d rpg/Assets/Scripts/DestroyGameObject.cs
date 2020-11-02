using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    /// <summary>
    /// The amount of time used to destroy object.
    /// </summary>
    [SerializeField] private float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);

    }

   
}
