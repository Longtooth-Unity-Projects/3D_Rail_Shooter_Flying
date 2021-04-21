using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandlerUsingCode : MonoBehaviour
{

    [Tooltip("In meters per second")]
    [SerializeField] float movementSpeed = 4f;

    //TODO replace this with dynamic clamp based on camera
    [SerializeField] float absoluteHorizontalClamp = 4.5f;
    //[SerializeField] float absoluteVerticalClamp = 3.3f;

    [SerializeField] private Inputs_3D_Rail_Shooter_Flying playerInput;
    private Vector2 moveVector;


    private void Awake()
    {
        playerInput = new Inputs_3D_Rail_Shooter_Flying();
    }

    private void OnEnable()
    {
        //register function to handle event, dont need to implement interface for this version
        playerInput.Player.Fire.performed += HandleFire;
        playerInput.Player.Move.performed += HandleMove;
        playerInput.Player.AltMove.performed += AltMove;

        playerInput.Player.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //used with context version of altmove
        float xOffset = moveVector.x * movementSpeed * Time.deltaTime;
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
        transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);
    }


    private void OnDisable()
    {
        //degister function to handle fire to performed event
        playerInput.Player.Fire.performed -= HandleFire;
        playerInput.Player.Move.performed -= HandleMove;
        playerInput.Player.AltMove.performed -= AltMove;

        playerInput.Player.Disable();
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        Debug.Log("Handle Move: " + moveVector.ToString());
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

