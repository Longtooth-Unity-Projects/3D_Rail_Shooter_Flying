using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In meters per second")]
    [SerializeField] float horizontalSpeed = 4f;

    //TODO replace this with dynamic clamp based on camera
    [SerializeField] float absoluteHorizontalClamp = 4.5f;
    [SerializeField] float absoluteVerticalClamp = 3.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO get rid of crossplatform manager package
        //TODO change input manager from both to new package

        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");

        //float horizontalThrow = Input.GetAxis("Horizontal");
        //float verticalThrow = Input.GetAxis("Vertical");

        float xOffset = horizontalThrow * horizontalSpeed * Time.deltaTime;
        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -absoluteHorizontalClamp, absoluteHorizontalClamp);

        transform.localPosition = new Vector3(newXPos, transform.localPosition.y, transform.localPosition.z);

        Debug.Log("Horizontal Throw:" + horizontalThrow);
        Debug.Log("Vertical Throw:" + verticalThrow);
        Debug.Log("Offset: " + xOffset);
    }
}
