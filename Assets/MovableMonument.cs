using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableMonument : MonoBehaviour
{

    public bool beingPushed;
    float xPos;
    float zPos;

    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(beingPushed == false)
        {
            transform.position = new Vector3(xPos, transform.position.y, zPos);
        }
        else
        {
            xPos = transform.position.x;
            zPos = transform.position.z;
        }
    }
}
