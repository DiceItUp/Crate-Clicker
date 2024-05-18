using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPos;
    private float bgWidth;
    void Start()
    {
        startPos = transform.position;
        bgWidth = GetComponent<BoxCollider>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - bgWidth/2)
        {
            transform.position = startPos;
        }
    }
}
