using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// Camera follow height
    /// </summary>
    [SerializeField]
    private float followHeight = 8f;

    /// <summary>
    /// Distance for follow camera
    /// </summary>
    [SerializeField]
    private float followDistance = 6f;

    /// <summary>
    /// Use player transform for current player position
    /// </summary>
    [SerializeField]
    private Transform player;

    /// <summary>
    /// target height
    /// </summary>
    private float targetHeight;

    /// <summary>
    /// Current height camera
    /// </summary>
    private float currentHeight;

    /// <summary>
    /// Current rotation camera
    /// </summary>
    private float currentRotation;

  

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCamera();
        ChangeCameraHeight();
    }

    private void ChangeCameraHeight()
    {
        
    }

    /// <summary>
    /// translate target height from playerposition add followheigt for correct position
    /// change the current rotation and transform
    /// change the currentheight transform to target height, mathf.lerp smooth zoom ins and outs
    /// change the rotation for camera
    /// change the target position to the player position, follow player.
    /// translate the camera to follow the player
    /// </summary>
    private void MoveCamera()
    {
        targetHeight = player.position.y + followHeight;
        currentRotation = transform.eulerAngles.y;
        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, 0.9f * Time.deltaTime);
        Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);

        Vector3 targetPosition = player.position - (euler * Vector3.forward) * followDistance;
        targetPosition.y = currentHeight;
        transform.position = targetPosition;
        transform.LookAt(player);
    }
}
