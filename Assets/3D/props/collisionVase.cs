using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionVase : MonoBehaviour
{
    // Start is called before the first frame update


    public Collider coll;

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
            other.attachedRigidbody.isKinematic = false;

    }
}
