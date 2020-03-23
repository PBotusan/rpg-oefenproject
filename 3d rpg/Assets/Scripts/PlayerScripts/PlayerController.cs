using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Player animator
    /// </summary>
    private Animator anim;

    /// <summary>
    /// Player charactercontroller used to as rigidbody to move player
    /// </summary>
    private CharacterController characterController;

    /// <summary>
    /// Movement speed playerchar
    /// </summary>
    private float movementSpeed = 5f;

    /// <summary>
    /// Player can move when true
    /// </summary>
    private bool canMove;

    /// <summary>
    /// Is true when player is done with movement
    /// </summary>
    private bool finishedMovement = true;

    /// <summary>
    /// Position to move to target
    /// </summary>
    private Vector3 targetPosition = Vector3.zero;

    /// <summary>
    /// move player to target position
    /// </summary>
    private Vector3 playerMove = Vector3.zero;

    private float playerMoveToPoint;

    /// <summary>
    /// standard gravity realworld
    /// </summary>
    private float gravity = 9.8f;

    /// <summary>
    /// 
    /// </summary>
    private float height;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();

    }


    void Update()
    {
        InputPlayer();
        characterController.Move(playerMove);

    }

    /// <summary>
    /// Moveplayer to mouse click position, Get left mousebutton from input.
    /// Get from camera, the raycast hit-position.
    /// move player to the terraincollider where player clicks.
    /// moves playermovepoint from position A to B.
    /// </summary>
    void InputPlayer()
    {
        int leftMouseButton = 0;
        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    playerMoveToPoint = Vector3.Distance(transform.position, hit.point);

                    if (playerMoveToPoint >= 1.0f)
                    {
                        canMove = true;
                        targetPosition = hit.point;
                    }
                }
            }
        } // if statement button up
        if (canMove)
        {
            anim.SetFloat("Walk", 1.0f);
            Vector3 targetLocation = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(targetLocation - transform.position),
                15.0f * Time.deltaTime);

            playerMove = transform.forward * movementSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) <= 0.5f)
            {
                canMove = false;
            }
            
        }
        else
        {
            playerMove.Set(0f, 0f, 0f);
            anim.SetFloat("Walk", 0f);
        }

    }

    /// <summary>
    /// if canMove is true.
    /// Translate player position -MovePlayer-
    /// setfloatspeed to 1, if it's one you can walk player is not idle anymore.
    /// moveplayer  to target position x and y, you can't go up in Y
    /// </summary>
    private void MovePlayer()
    {
        
    } 

}
