using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public Transform aimPosition;
    public GameObject currentPos;
    GameObject currentTarget;
    public float distance = 5f;

    bool IsAiming;

    private void Start()
    {
        currentPos.transform.position = aimPosition.position;

    }

    private void Update()
    {
        CheckTarget();

        if (IsAiming)
            AutoAiming();
    }

    private void CheckTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast (transform.position, transform.forward, out hit, distance))
        {
            if(hit.collider.gameObject.tag == "Enemy")
            {
                if (!IsAiming)
                {
                    currentTarget = hit.transform.gameObject;
                    IsAiming = true;
                }
                else
                {
                    currentTarget = null;
                    IsAiming = false;
                }
            }
        }
    }

    private void AutoAiming()
    {
        currentPos.transform.LookAt(currentTarget.transform);
    }
}
