using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPointer : MonoBehaviour
{
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyMousePointer();
    }

    /// <summary>
    /// Destroy mouse pointer when player position is more than 1.1f
    /// </summary>
    private void DestroyMousePointer()
    {
        Destroy(gameObject, 1f);
    }
}
