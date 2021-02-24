/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class MovePractice : MonoBehaviour
{

    [Tooltip("In meters per second")]
    [SerializeField] float movementSpeed = 4f;

    //TODO replace this with dynamic clamp based on camera
    [SerializeField] float absoluteHorizontalClamp = 4.5f;
    [SerializeField] float absoluteVerticalClamp = 3.3f;

    //[SerializeField] private Inputs_3D_Rail_Shooter_Flying playerInput;
    private Vector2 moveVector;


    private void Awake()
    {
        //playerInput = new Inputs_3D_Rail_Shooter_Flying();
        //playerInput.PlayerControls.Move.performed += ctx => moveVector ctx.ReadVAlue<Vector2)();
    }

    private void OnEnable()
    {
        //register function to handle fire to performed event, dont need to implement interface for this version
        *//*        playerInput.Player.Fire.performed += HandleFire;
                playerInput.Player.Move.performed += HandleMove;
                playerInput.Player.AltMove.performed += AltMove;*//*

        //playerInput.Player.SetCallbacks(this); //need interface Inputs_3D_Rail_Shooter_Flying.IPlayerActions

        //playerInput.Player.Enable();  //this enablesall actions under Player action map
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TODO get rid of crossplatform manager package
        //TODO change input manager from both to new package

        //unity cross platform package
        //float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        //float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

        //unity old input system
        *//*        float horizontalThrow = Input.GetAxis("Horizontal");
                float verticalThrow = Input.GetAxis("Vertical");
                float xOffset = horizontalThrow * movementSpeed * Time.deltaTime;
                float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
                transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);*//*

        //Debug.Log("Horizontal Throw:" + horizontalThrow);
        //Debug.Log("Vertical Throw:" + verticalThrow);
        //Debug.Log("Offset: " + xOffset);










        //used with context version of altmove
        float xOffset = moveVector.x * movementSpeed * Time.deltaTime;
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
        transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);

    }


    private void OnDisable()
    {
        //degister function to handle fire to performed event
        *//*        playerInput.Player.Fire.performed -= HandleFire;
                playerInput.Player.Move.performed -= HandleMove;
                playerInput.Player.AltMove.performed -= AltMove;

                playerInput.Player.Disable();*//*
    }

    // If you are interested in the value from the control that triggers an action,
    // you can declare a parameter of type InputValue.
    public void OnMove(InputValue value)
    {
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        Vector2 inputVector = value.Get<Vector2>();



        *//*        float xOffset = inputVector.x * movementSpeed * Time.deltaTime;
                float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
                transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);*//*

        Debug.Log("Move inputVector: " + inputVector.ToString());
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        *//*        moveVector = context.ReadValue<Vector2>();
                Debug.Log("Handle Move: " + moveVector.ToString());*//*
    }

    public void OnAltMove(InputValue value)
    {
        *//*        moveVector = value.Get<Vector2>();
                Debug.Log("On Alt Move: " + moveVector.ToString());*//*
    }

    public void AltMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        Debug.Log("Alt Move: " + moveVector.ToString());
    }

    public void OnFire()
    {
        Debug.Log("OnFire!!!");
    }

    public void HandleFire(InputAction.CallbackContext context)
    {
        Debug.Log("HandleFire!!!");
    }

}

*/