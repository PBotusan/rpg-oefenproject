﻿using UnityEngine.AI;
using UnityEngine;
using System;

/// <summary>
/// All the enemyStats.
/// </summary>
public enum EnemyState
{
    IDLE,
    WALK,
    RUN,
    PAUSE,
    GOBACK,
    ATTACK,
    DEATH
}




public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Distance that the enemy can attack the player
    /// </summary>
    private float attackDistance = 1.5f;


    /// <summary>
    /// Alert enemy when close enough. Attacks player on sight.
    /// </summary>
    private float alertAttackDistance = 8f;


    /// <summary>
    /// Enemy following the player distance
    /// </summary>
    private float followDistance = 15f;


    /// <summary>
    /// The distance between player and enemy. used to calculate followDistance, attackDistance.
    /// </summary>
    private float enemyToPlayerDistance;

    /// <summary>
    /// Current state of the enemy
    /// </summary>
    private EnemyState enemyCurrentState = EnemyState.IDLE;

    /// <summary>
    /// Last state of the enemy
    /// </summary>
    private EnemyState enemyLastState = EnemyState.IDLE;

    /// <summary>
    /// Get and Set the enemyCurrentState to make it public available.
    /// </summary>
    public EnemyState EnemyCurrentState{ get { return enemyCurrentState; }  set { enemyCurrentState = value; } }


    /// <summary>
    /// The target used to target the player
    /// </summary>
    [SerializeField] Transform playerTarget;


    /// <summary>
    /// InitialPosition of
    /// </summary>
    private Vector3 initialPosition;


    /// <summary>
    /// Enemy move speed
    /// </summary>
    private float moveSpeed = 2f;


    /// <summary>
    /// Enemy walking speed
    /// </summary>
    private float walkSpeed = 1f;


    /// <summary>
    /// Charactercontroller used for enemy
    /// </summary>
    [SerializeField] CharacterController characterController;


    /// <summary>
    /// Move to position
    /// </summary>
    private Vector3 MoveTo = Vector3.zero;


    /// <summary>
    /// Current attack time
    /// </summary>
    private float currentAttackTime;


    /// <summary>
    /// Amount of time to wait for attack. 
    /// </summary>
    private float waitAttackTime = 1f;


    /// <summary>
    /// animator used by enemy
    /// </summary>
    [SerializeField] Animator animator;


    /// <summary>
    /// Boolean used to show if animation is finished.
    /// </summary>
    private bool finished_Animation = true;

    /// <summary>
    /// boolean used to show if movement is finished.
    /// </summary>
    private bool finishedMovement = true;


    /// <summary>
    /// navigation agent used by enemy.
    /// </summary>
    [SerializeField] NavMeshAgent navAgent;

    /// <summary>
    /// Navigate to this point.
    /// </summary>
    private Vector3 navigateTo;


    /// <summary>
    /// Used when starting
    /// </summary>
    void Awake()
    {
        try
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            navAgent = GetComponent<NavMeshAgent>();
            characterController.GetComponent<CharacterController>();
            animator.GetComponent<Animator>();

            initialPosition = transform.position;
            navigateTo = transform.position;
        }
        catch (System.Exception exception)
        {

            Debug.Log(exception);
            Debug.Log(exception.Message);
            Debug.Log(exception.Source);
        }
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        try
        {
            CurrentEnemyState();

        }
        catch (System.Exception exception)
        {

            Debug.Log(exception);
        }
        
    }

    private void CurrentEnemyState()
    {
        if (EnemyCurrentState != EnemyState.DEATH)
        {
            enemyCurrentState = SetEnemyState(enemyCurrentState, enemyLastState, enemyToPlayerDistance);

            if (finishedMovement)
            {
                GetStateControl(enemyCurrentState);
            }
            else
            {
                if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    finishedMovement = true;

                }
                else if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK1") || animator.GetCurrentAnimatorStateInfo(0).IsTag("Atk2"))
                {
                    animator.SetInteger("Atk", 0);
                }
                else
                {
                    animator.SetBool("Death", true);
                    characterController.enabled = false;
                    navAgent.enabled = false;

                    if (!animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                    {
                        Destroy(gameObject, 2f);
                    }
                }
            }

        }
        else
        {

        }

    }


    private EnemyState SetEnemyState(EnemyState currentState, EnemyState lastState, float enemyToPlayerDistance)
    {
        enemyToPlayerDistance = Vector3.Distance(transform.position, playerTarget.position);

        //initial distance of vector3
        float initialDistance = Vector3.Distance(initialPosition, transform.position);

        //Calculate if the initial distance is bigger than the followdistance, stop following = GOBACK
        if (initialDistance > followDistance)
        {
            lastState = currentState;
            currentState = EnemyState.GOBACK;
        }
        else if (enemyToPlayerDistance <= attackDistance)
        {
            lastState = currentState;
            currentState = EnemyState.ATTACK;
        }
        else if (enemyToPlayerDistance >= alertAttackDistance && lastState == EnemyState.PAUSE || lastState == EnemyState.ATTACK)
        {
            lastState = currentState;
            currentState = EnemyState.PAUSE;
        }
        else if (enemyToPlayerDistance <= alertAttackDistance && enemyToPlayerDistance > attackDistance)
        {
            if (currentState != EnemyState.GOBACK || lastState == EnemyState.WALK)
            {
                lastState = currentState;
                currentState = EnemyState.PAUSE;
            }
        }
        else if (enemyToPlayerDistance > alertAttackDistance && lastState != EnemyState.GOBACK && lastState != EnemyState.PAUSE)
        {
            lastState = currentState;
            currentState = EnemyState.WALK;
        }

        return currentState;
    }

    private void GetStateControl(EnemyState enemyCurrentState)
    {
        if (EnemyCurrentState == EnemyState.RUN || enemyCurrentState == EnemyState.PAUSE)
        {
            if (enemyCurrentState != EnemyState.ATTACK)
            {
                Vector3 targetPosition = new Vector3(playerTarget.position.x, playerTarget.position.y, playerTarget.position.z);


                if (Vector3.Distance(transform.position, targetPosition) >= 2.1f)
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);

                    navAgent.SetDestination(targetPosition);
                }
                
            }

        }
        else if (EnemyCurrentState == EnemyState.ATTACK)
        {
            animator.SetBool("Run", false);
            MoveTo.Set(0f, 0f, 0f);

            navAgent.SetDestination(transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTarget.position - transform.position), 5f * Time.deltaTime);

            //Check if you  can attack player
            if (currentAttackTime >= waitAttackTime)
            {
                int attackRange = UnityEngine.Random.Range(1, 3);

                animator.SetInteger("Atk", attackRange);
                Debug.Log("Enemy attacks with = " + attackRange);

                finished_Animation = false;
                currentAttackTime = 0f;
            }
            else
            {
                animator.SetInteger("atk", 0);
                currentAttackTime += Time.deltaTime;

            }
        }
        else if (EnemyCurrentState == EnemyState.GOBACK)
        {
            animator.SetBool("Run", true);
            Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);

            navAgent.SetDestination(targetPosition);

            if (Vector3.Distance(targetPosition, initialPosition) <= 3.5f)
            {
                enemyLastState = enemyCurrentState;
                enemyCurrentState = EnemyState.WALK;
            }
        }
        else if (EnemyCurrentState == EnemyState.WALK)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);

            //random path walking
            if (Vector3.Distance(transform.position, navigateTo) <= 2f)
            {
                navigateTo.x = UnityEngine.Random.Range(initialPosition.x - 10f, initialPosition.x + 10f);
                navigateTo.z = UnityEngine.Random.Range(initialPosition.z - 5f, initialPosition.z + 5f);
   
            }
            else
            {
                navAgent.SetDestination(navigateTo);
               
            }
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);

            MoveTo.Set(0f, 0f, 0f);
            navAgent.isStopped = true;

        }

        

    }


}
