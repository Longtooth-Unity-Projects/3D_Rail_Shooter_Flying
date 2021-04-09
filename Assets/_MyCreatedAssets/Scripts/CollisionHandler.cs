using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + " --collided with--" + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} **triggered by** {other.gameObject.name}");
    }
}
