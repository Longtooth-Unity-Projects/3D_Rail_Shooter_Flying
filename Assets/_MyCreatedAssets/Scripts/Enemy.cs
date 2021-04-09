using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int scoreValuePerHit = 5;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject deathFX;

    //chached references
    ScoreBoard scoreBoard;
    private GameObject container;
    string containerName = "ContainerForRuntimeSpawns";



    private void Start()
    {
        //add rigid body to enemy via code
        //gameObject.AddComponent<Rigidbody>();
        //GetComponent<Rigidbody>().useGravity = false;


        //container = GameObject.Find(containerName);
        container = GameObject.FindWithTag(containerName);
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Instantiate(hitVFX, transform.position, Quaternion.identity, container.transform);

        hitPoints--;
        scoreBoard.adjustScore(scoreValuePerHit);

        if (hitPoints <= 0)
        {
            Instantiate(deathFX, transform.position, Quaternion.identity, container.transform);
            Destroy(gameObject);
        }
    }
}
