using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerSingleton : MonoBehaviour
{

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayerSingleton>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
