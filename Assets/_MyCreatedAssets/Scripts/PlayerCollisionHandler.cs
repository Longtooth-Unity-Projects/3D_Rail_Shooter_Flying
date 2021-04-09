using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private float sceneLoadDelay = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
        FindObjectOfType<SceneLoader>().RestartScene(sceneLoadDelay);
    }

    private void StartCrashSequence()
    {
        explosionVFX.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerInputIndividual>().enabled = false;
    }
}
