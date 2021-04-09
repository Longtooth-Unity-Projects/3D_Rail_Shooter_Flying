using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("Must Assign These Fields")]
    [SerializeField] private GameObject[] projectileLaunchers;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform topTurret;
    [SerializeField] private Transform TurretTopProjLaunchpointPort;
    [SerializeField] private Transform TurretTopProjLaunchpointStarboard;

    [Header("General Setup Settings")]
    [Tooltip("In meters per second")]
    [SerializeField] private float movementSpeed = 14f;
    //TODO replace this with dynamic clamp based on camera
    [Tooltip("Horizontal movement limit")]
    [SerializeField] private float absoluteHorizontalClamp = 11.2f;
    [Tooltip("Vertical movement limit")]
    [SerializeField] private float absoluteVerticalClamp = 7.4f;
    [SerializeField] private float turretRotateSpeed = 1.0f;
    [SerializeField] private float turretFireDelay = 0.1f;

    [Header("Position Modifiers")]
    [SerializeField] private float positionPitchFactor = -1f;
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlRollFactor = -20f;




    //TODO public for debugging, make private before release
    public Vector2 cursorVector;
    public Vector3 aimVector;
    public Vector3 updateVector;

    //used to ensure same instance of move coroutine that is called during start phase is canceled during the cancel phase
    private Coroutine moveCoroutineReference;
    private Coroutine fireCoroutineReference;
    private float analogBreakPoint = 0.1f;
    private Vector2 throwVector;




    //for holding instantiated objects such as projectiles
    string containerName = "ContainerForRuntimeSpawns";
    private GameObject container;

    private Camera mainCamera;


    //TODO these values are for testing only remove / replace for final
    [SerializeField] private float aimPointDistance = 25f;





    private void Start()
    {
        mainCamera = Camera.main;
        container = GameObject.Find(containerName);
    }
    private void Update()
    {
        //updateVector = mainCamera.ScreenToWorldPoint(new Vector3(cursorVector.x, cursorVector.y, mainCamera.farClipPlane));
        updateVector = mainCamera.ScreenToWorldPoint(new Vector3(cursorVector.x, cursorVector.y, aimPointDistance));

        MoveTurret();
    }


    private void MoveTurret()
    {
        //vector3 rotate towards
        //float singleStep = turretRotateSpeed * Time.deltaTime;
        //these calculations only needed if you want to gradually rotate
        // Vector3 newDirection = Vector3.RotateTowards(topTurret.forward, updateVector, singleStep, 0.0f);
        // Calculate a rotation a step closer to the target and applies rotation to this object,, newDirection is the forward vector
        //topTurret.localRotation = Quaternion.LookRotation(newDirection);

        //no need to calculate gradual rotation for this, just used for testing
        //TODO remove this when testing complete
        //topTurret.localRotation = Quaternion.LookRotation(updateVector);

        topTurret.LookAt(updateVector);   //points forward vector to look at target

        Debug.DrawLine(topTurret.position, aimVector, Color.green);
        Debug.DrawLine(topTurret.position, updateVector, Color.blue);
        Debug.DrawRay(topTurret.position, topTurret.forward * 15, Color.red);
    }


    //player input handlers
    public void OnAim(InputAction.CallbackContext context)
    {
        cursorVector = context.ReadValue<Vector2>();
        aimVector = mainCamera.ScreenToWorldPoint(new Vector3(cursorVector.x, cursorVector.y, mainCamera.farClipPlane));
        Debug.Log("OnAim!!!");
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                fireCoroutineReference = StartCoroutine(ContinuousFire());
                break;
            case InputActionPhase.Canceled:
                StopCoroutine(fireCoroutineReference);
                break;
            default:
                break;
        }


        IEnumerator ContinuousFire()
        {
            while (true)
            {
                Instantiate(projectile, TurretTopProjLaunchpointPort.position, TurretTopProjLaunchpointPort.rotation, container.transform).SetActive(true);
                Instantiate(projectile, TurretTopProjLaunchpointStarboard.position, TurretTopProjLaunchpointStarboard.rotation, container.transform).SetActive(true);

                yield return new WaitForSeconds(turretFireDelay);
            }
        }
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("OnLook!!!");
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        throwVector = context.ReadValue<Vector2>();
        Debug.Log("OnMove context:  " + context.phase.ToString() + " Value: " + throwVector.ToString());

        switch (context.phase)
        {
            case InputActionPhase.Started:
                //TODO see if this check is necessary
                //ensure the prior coroutine reference isnt lost causing a memory leak 
                if (moveCoroutineReference != null)
                    StopCoroutine(moveCoroutineReference);

                moveCoroutineReference = StartCoroutine(MovePlayer());
                break;
            case InputActionPhase.Canceled:
                StopCoroutine(moveCoroutineReference);
                //TODO see if we can make the return to no rotation gradual
                //need to reset rotation as method is not called in update
                //TODO this was part of the ship roation code, need to fix this
                //transform.localRotation = Quaternion.Euler(transform.localPosition.y * positionPitchFactor, transform.localRotation.y, transform.localRotation.z);
                break;
            default:
                break;
        }


        IEnumerator MovePlayer()
        {
            while (true)
            {
                Debug.Log("Move Player Coroutine Vector Value" + throwVector.ToString());
                float xOffset = throwVector.x * movementSpeed * Time.deltaTime;
                float yOffset = throwVector.y * movementSpeed * Time.deltaTime;

                float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
                float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -absoluteVerticalClamp, absoluteVerticalClamp);

                transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);

                //change rotation so ship is not alway ponted to center of horizon
                float pitch = transform.localPosition.y * positionPitchFactor + (throwVector.y * controlPitchFactor);
                float yaw = transform.localPosition.x * positionYawFactor;
                float roll = throwVector.x * controlRollFactor;

                transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

                yield return new WaitForEndOfFrame();
            }          
        }
    }// end of method OnMove


    public void OnAltMove(InputAction.CallbackContext context)
    {
        Debug.Log("Alt Move context:  " + context.phase.ToString() + " Value: " + throwVector.ToString());
    }




}
