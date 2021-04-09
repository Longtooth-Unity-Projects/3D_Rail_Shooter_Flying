using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputIndividual : MonoBehaviour
{
    //TODO remove thsese and implement callbacks or at least use input action set, i.e. on demand as opposed to checking every update
    [SerializeField] private InputAction playerMovement;
    [SerializeField] private InputAction playerFire;
    [SerializeField] private InputAction playerAim;

    [Header("Must Assign These Fields")]
    [Tooltip("The launcher where the particle projectile is going to spawn")]
    [SerializeField] private GameObject[] particleProjectileLaunchers;
    [Tooltip("Turret Game Object")]
    [SerializeField] private Transform topTurret;
    [Tooltip("Port Turret Launchpoint")]
    [SerializeField] private Transform TurretTopProjLaunchpointPort;
    [Tooltip("Starboard Turret Launchpoint")]
    [SerializeField] private Transform TurretTopProjLaunchpointStarboard;

    [Header("General Setup Settings")]
    [Tooltip("In meters per second")]
    [SerializeField] private float movementSpeed = 14f;
    //TODO replace this with dynamic clamp based on camera
    [Tooltip("Horizontal movement limit")]
    [SerializeField] private float absoluteHorizontalClamp = 11.2f;
    [Tooltip("Vertical movement limit")]
    [SerializeField] private float absoluteVerticalClamp = 7.4f;

    [Header("Position Modifiers")]
    [SerializeField] private float positionPitchFactor = -1f;
    [SerializeField] private float controlPitchFactor = -10f;
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlRollFactor = -20f;


    private float analogBreakPoint = 0.1f;
    private Vector2 throwVector;
    private Camera mainCamera;

    //TODO public for debugging, make private before release
    public Vector2 cursorVector;
    public Vector3 aimVector;
    public Vector3 updateVector;

    //TODO these values are for testing only remove / replace for final
    [SerializeField] private float aimPointDistance = 25f;

    private void OnEnable()
    {
        playerMovement.Enable();
        playerFire.Enable();
        playerAim.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
        playerFire.Disable();
        playerAim.Disable();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // TODO replace this with callbacks, i.e. don't check every update
        ProcessTranslation();
        ProcessRotation();

        ProcessAim();
        ProcessTurretMovement();
        ProcessFire(playerFire.ReadValue<float>() > analogBreakPoint);
    }


    //custom methods
    private void ProcessTranslation()
    {
        throwVector = playerMovement.ReadValue<Vector2>();

        float xOffset = throwVector.x * movementSpeed * Time.deltaTime;
        float yOffset = throwVector.y * movementSpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -absoluteVerticalClamp, absoluteVerticalClamp);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + (throwVector.y * controlPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = throwVector.x * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessAim()
    {
        cursorVector = playerAim.ReadValue<Vector2>();
        aimVector = mainCamera.ScreenToWorldPoint(new Vector3(cursorVector.x, cursorVector.y, mainCamera.farClipPlane));
    }


    private void ProcessTurretMovement()
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

        //updateVector = mainCamera.ScreenToWorldPoint(new Vector3(cursorVector.x, cursorVector.y, mainCamera.farClipPlane));
        updateVector = mainCamera.ScreenToWorldPoint(new Vector3(cursorVector.x, cursorVector.y, aimPointDistance));
        topTurret.LookAt(updateVector);   //points forward vector to look at target

        Debug.DrawLine(topTurret.position, aimVector, Color.green);
        Debug.DrawLine(topTurret.position, updateVector, Color.blue);
        Debug.DrawRay(topTurret.position, topTurret.forward * 15, Color.red);
    }

    private void ProcessFire(bool isActive)
    {
        foreach (GameObject launcher in particleProjectileLaunchers)
        {
            var emissionModule = launcher.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
