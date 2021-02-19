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
    public bool IsThrowable;
    public float grabDistance = 2f;

    void Grab()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            Debug.DrawRay(origin, direction * grabDistance, Color.red);

            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(directionRay, out hit, grabDistance))
            {
                if (hit.collider != null && hit.collider.tag == "MobileObject")
                {
                    CarryObject = true;
                    IsThrowable = true;
                    {
                        if (CarryObject == true)
                        {
                            Item = hit.collider.gameObject;
                            Item.transform.SetParent(ObjectHolder);
                            Item.gameObject.transform.position = ObjectHolder.position;
                            Item.GetComponent<Rigidbody>().isKinematic = true;
                            Item.GetComponent<Rigidbody>().useGravity = false;
                        }
                    }
                }
            }
        }
        else if (Input.GetButtonUp("Fire2"))
            {
                CarryObject = false;
                IsThrowable = false;

                ObjectHolder.DetachChildren();
                if (Item.gameObject.tag != "MobileObject")
                {
                    StartCoroutine(Throw());
                }
            }
        }
    IEnumerator Throw()
    {
        yield return null;
        Item.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

}


