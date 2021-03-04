using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 225.0f;
    [SerializeField] private float projectileLife = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBulletAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }


    IEnumerator DestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(projectileLife);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.ToString());
        Destroy(gameObject);
    }
}
