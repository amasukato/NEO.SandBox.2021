using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThrow : MonoBehaviour
{
    // Grab & Thrown
    public Transform ObjectHolder;
    public float ThrowForce;
    public bool CarryObject;
    public GameObject Item;
    private float sphereCastRadius = 1.5f;
    public float grabDistance = 3.5f;

    public CharController Player;

    public void Start()
    {
        Player = GetComponent<CharController>();
    }
    public void Update()
    {
        Grab();
    }
    void Grab()
    {
        if (Input.GetButtonDown("Fire6"))
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            Debug.DrawRay(origin, direction * grabDistance, Color.red);

            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);

            if (Physics.SphereCast(directionRay, sphereCastRadius, out hit, grabDistance))
            {
                if (hit.collider != null && hit.collider.tag == "MobileObject")
                {
                    CarryObject = true;
                    if(CarryObject == true)
                    {
                        Item = hit.collider.gameObject;
                        Item.transform.parent = ObjectHolder;
                        Item.gameObject.transform.position = ObjectHolder.position;
                        Item.GetComponent<Rigidbody>().isKinematic = true;
                        Item.GetComponent<Rigidbody>().useGravity = false;
                    }
                    Player.PlayerState = CharController.State.Grabing;
                }
                else
                {
                    CarryObject = false;
                }

            }
           // Player.PlayerState = CharController.State.Grabing;
        }


        else if (Input.GetButtonUp("Fire6"))
            {
                CarryObject = false;
                
                Item.GetComponent<Rigidbody>().isKinematic = false;
                Item.GetComponent<Rigidbody>().useGravity = true;

                Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);

                //Item.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ObjectHolder.DetachChildren();
            //Player.PlayerState = CharController.State.Idle;

        }
        else
        {
            CarryObject = false;
            //Player.PlayerState = CharController.State.Idle;
        }

    }

}


