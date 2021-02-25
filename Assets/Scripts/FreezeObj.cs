using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeObj : MonoBehaviour
{
    public Rigidbody rb;


    private float groundDistance = 1.5f;
    public LayerMask groundMask;
    [SerializeField] private bool IsOnTheGround;


    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        IsOnTheGround = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if(IsOnTheGround == true)
        {
            FreezePos();
        }
    }

    public void FreezePos()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
