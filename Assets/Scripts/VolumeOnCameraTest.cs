using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeOnCameraTest : MonoBehaviour
{


    // Start is called before the first frame update
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length > 1)
        {
            Destroy(gameObject);
        }
        
            
        
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
