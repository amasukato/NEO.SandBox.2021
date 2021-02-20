using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{

    public Transform player;
    public Transform objectHolder;

    public float throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;

    public AudioClip[] soundToPlay;
    private AudioSource audio;

    public int damage;
    private bool touched = false;

    void Start()
    {
        
    }

    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = objectHolder;

            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            beingCarried = false;
            GetComponent<Rigidbody>().AddForce(objectHolder.forward * throwForce);
        }
    }

    private void OnTriggerEnter()
    {
        if (beingCarried)
        {
           // touched = true;
        }
    }
}
