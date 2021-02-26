using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In meters per second")]
    [SerializeField] private float movementSpeed = 14f;

    //TODO replace this with dynamic clamp based on camera
    [SerializeField] private float absoluteHorizontalClamp = 5.25f;
    [SerializeField] private float absoluteVerticalClamp = 3.3f;

    //used to ensure same instance of move coroutine that is called during start phase is canceled during the cancel phase
    private Coroutine moveCoroutineReference;
    private Vector2 moveVector;


    //input handlers
    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        Debug.Log("OnMove context:  " + context.phase.ToString() + " Value: " + moveVector.ToString());

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
                break;
            default:
                break;
        }


        IEnumerator MovePlayer()
        {
            while (true)
            {
                Debug.Log("Move Player Coroutine Vector Value" + moveVector.ToString());
                float xOffset = moveVector.x * movementSpeed * Time.deltaTime;
                float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);
                transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);
                yield return new WaitForEndOfFrame();
            }
        }
    }// end of method OnMove

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
