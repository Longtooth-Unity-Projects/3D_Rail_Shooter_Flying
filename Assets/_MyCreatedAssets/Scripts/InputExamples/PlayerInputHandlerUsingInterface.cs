using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandlerUsingInterface : MonoBehaviour, Inputs_3D_Rail_Shooter_Flying.IPlayerActions
{

    [Tooltip("In meters per second")]
    [SerializeField] float movementSpeed = 4f;

    //TODO replace this with dynamic clamp based on camera
    [SerializeField] float absoluteHorizontalClamp = 4.5f;
    //[SerializeField] float absoluteVerticalClamp = 3.3f;


    [SerializeField] private Inputs_3D_Rail_Shooter_Flying playerInput;
    private Vector2 moveVector;


    private void OnEnable()
    {
        //register function to handle fire to performed event
        playerInput.Player.SetCallbacks(this); //need interface Inputs_3D_Rail_Shooter_Flying.IPlayerActions
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
        playerInput.Player.Disable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove!!!");
    }

    public void OnAltMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        Debug.Log("Alt Move: " + moveVector.ToString());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("OnLook!!!");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("OnFire!!!");
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}

