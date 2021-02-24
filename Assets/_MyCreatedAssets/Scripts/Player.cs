﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In meters per second")]
    [SerializeField] private float movementSpeed = 15f;

    //TODO replace this with dynamic clamp based on camera
    [SerializeField] private float absoluteHorizontalClamp = 5.25f;
    [SerializeField] private float absoluteVerticalClamp = 3.3f;
    private Vector2 moveVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }


    private void MovePlayer()
    {
        float xOffset = moveVector.x * movementSpeed * Time.deltaTime;
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
        transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);
    }
     




    //input handlers
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        Debug.Log("OnMove context:  " + context.phase.ToString() + " Value: " + moveVector.ToString());
    }

    public void OnAltMove(InputAction.CallbackContext context)
    {
        Debug.Log("Alt Move context:  " + context.phase.ToString() + " Value: " + moveVector.ToString());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("OnLook!!!");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("OnFire!!!");
    }
}
