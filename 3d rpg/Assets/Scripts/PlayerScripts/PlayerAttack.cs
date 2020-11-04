﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 

public class PlayerAttack : MonoBehaviour
{

    private UserInputStrings.KeyboardNumberStrings keyboardString;

    /// <summary>
    /// All the attacks that are active in bar, use it to put in fadeImages
    /// </summary>
    [SerializeField]
    private Image fillWaitImage1;

    [SerializeField]
    private Image fillWaitImage2;

    [SerializeField]
    private Image fillWaitImage3;

    [SerializeField]
    private Image fillWaitImage4;

    [SerializeField]
    private Image fillWaitImage5;

    [SerializeField]
    private Image fillWaitImage6;

    /// <summary>
    /// FadeImages when player clicks the attack button
    /// </summary>
    private int[] fadeImages = new int[] {0,0,0,0,0,0,0};


    /// <summary>
    /// Player animator
    /// </summary>
    private Animator playerAnimator;


    /// <summary>
    /// player controller script, use to move plater 'control'.
    /// </summary>
    private PlayerController playerMove;


    /// <summary>
    /// shows if player can attack true/false
    /// </summary>
    private bool canAttack = true;


    /// <summary>
    /// Stores value of used key: 1-6| stored as array value: 0-5
    /// </summary>
    private int keyboardButton;

    private int attackImageCooldown;


    // Start is called before the first frame update
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerCanAttack();
        RotatePlayer();
    }


    /// <summary>
    /// Check if the player is allowed to attack.
    /// </summary>
    private void CheckIfPlayerCanAttack()
    {
        if (!playerAnimator.IsInTransition(0) && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            canAttack = true;

        }
        else
        {
            canAttack = false;
        }


        CheckPlayerAttackInput();
        CheckForCooldown();
    }

    /// <summary>
    /// Rotate player by changing the targetposition->'player'. change the targets position with new vector3 to transform to the mouse rotation.
    /// You can change position by pressing space/scrollwheel button
    /// </summary>
    private void RotatePlayer()
    {
        if (Input.GetButton("Rotate"))
        {
            Vector3 targetPosition = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 15f * Time.deltaTime);
        }
    }


    /// <summary>
    /// Player input checker, check which attack is used by player. Keyboardbutton is used to check which button the player used to attack is also linked to attack type.
    /// </summary>
    private void CheckPlayerAttackInput()
    {
        if (playerAnimator.GetInteger("Attack") == 0)
        {
            playerMove.FinishedMovement = false;

            if (!playerAnimator.IsInTransition(0) && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                playerMove.FinishedMovement = true;
            }
        }


        if (Input.GetButtonDown(keyboardString.Keyboard1))
        {
            playerMove.TargetPosition = transform.position;
            keyboardButton = 0;

            UseAttack(keyboardButton);
            Debug.Log("Key 1 pressed");
        }
        else if (Input.GetButtonDown(keyboardString.Keyboard2))
        {
            playerMove.TargetPosition = transform.position;
            keyboardButton = 1;

            UseAttack(keyboardButton);
            Debug.Log("Key 2 pressed");
        }
        else if (Input.GetButtonDown(keyboardString.Keyboard3))
        {
            playerMove.TargetPosition = transform.position;
            keyboardButton = 2;

            UseAttack(keyboardButton);
            Debug.Log("Key 3 pressed");
        }
        else if (Input.GetButtonDown(keyboardString.Keyboard4))
        {
            playerMove.TargetPosition = transform.position;
            keyboardButton = 3;

            UseAttack(keyboardButton);
            Debug.Log("Key 4 pressed");
        }
        else if (Input.GetButtonDown(keyboardString.Keyboard5))
        {
            playerMove.TargetPosition = transform.position;
            keyboardButton = 4;

            UseAttack(keyboardButton);
            Debug.Log("Key 5 pressed");
        }
        else if (Input.GetButtonDown(keyboardString.Keyboard6))
        {
            playerMove.TargetPosition = transform.position;
            keyboardButton = 5;

            UseAttack(keyboardButton);
            Debug.Log("Key 6 pressed");
        }
        else if (Input.GetButtonDown("MouseRight"))
        {
            playerMove.TargetPosition = transform.position;
            AutoAttack();

            Debug.Log("Leftmousebutton pressed");
        }
        else
        {
            playerAnimator.SetInteger("Attack", 0);
        }

    }

    private void AutoAttack()
    {
        if (playerMove.FinishedMovement && canAttack)
        {
            //keyboardbutton is based on array value. need + 1 to make it correct. 
            playerAnimator.SetInteger("Attack", 0);
        }
    }

    private void UseAttack(int keyboardButton)
    {
        if (playerMove.FinishedMovement && fadeImages[keyboardButton] != 1 && canAttack)
        {
            //keyboardbutton is based on array value. need + 1 to make it correct. 
            var attackBasedOnInput = keyboardButton + 1;

            fadeImages[keyboardButton] = 1;
            playerAnimator.SetInteger("Attack", attackBasedOnInput);

            
        }
    }

    private void CheckForCooldown()
    {
        //todo dynamisch maken
        if (fadeImages[0] == 1)
        {
            if (CooldownAttackIcon(fillWaitImage1, 1.0f))
            {
                // change to false
                fadeImages[0] = 0;
                playerAnimator.SetInteger("Atk", 1);
            }

        }

        if (fadeImages[1] == 1)
        {
            if (CooldownAttackIcon(fillWaitImage2, 1.0f))
            {
                // change to false
                fadeImages[1] = 0;
                playerAnimator.SetInteger("Atk", 2);
            }

        }

        if (fadeImages[2] == 1)
        {
            if (CooldownAttackIcon(fillWaitImage3, 1.0f))
            {
                // change to false
                fadeImages[2] = 0;
                playerAnimator.SetInteger("Atk", 3);
            }

        }

        if (fadeImages[3] == 1)
        {
            if (CooldownAttackIcon(fillWaitImage4, 1.0f))
            {
                // change to false
                fadeImages[3] = 0;
                playerAnimator.SetInteger("Atk", 4);
            }

        }

        if (fadeImages[4] == 1)
        {
            if (CooldownAttackIcon(fillWaitImage5, 1.0f))
            {
                // change to false
                fadeImages[4] = 0;
                playerAnimator.SetInteger("Atk", 5);
            }

        }

        if (fadeImages[5] == 1)
        {
            if (CooldownAttackIcon(fillWaitImage6, 1.0f))
            {
                // change to false
                fadeImages[5] = 0;
                playerAnimator.SetInteger("Atk", 6);
            }

        }
    }

    private bool CooldownAttackIcon(Image fadeImage, float fadeTime)
    {
        bool faded = false;

        if (fadeImage == null)
            return faded;

        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImage.fillAmount <= 0.0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }
}


