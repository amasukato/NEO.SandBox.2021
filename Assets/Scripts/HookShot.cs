using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    [SerializeField] private Transform DebugHitPointTransform;
    [SerializeField] private Transform hookshotTransform;

    private CharacterController characterController;

    public float HookshotDistance;

    private State state;
    private Vector3 hookshotPosition;
    private float hookshotSize;
    private float sphereCastRadius = 1.5f;

    public CharController Player;

    private enum State
    {
        Normal,
        HookshotThrown,
        HookShotFlyingPlayer,

    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Player = GetComponent<CharController>();
        state = State.Normal;
        hookshotTransform.gameObject.SetActive(false);

    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HookShotStart();

                break;

            case State.HookshotThrown:
                HookshotThrown();

                break;

            case State.HookShotFlyingPlayer:
                HookShotMovement();

                break;
        }

    }



    private void HookShotStart()
    {
        if (Input.GetButtonDown("Fire5"))
        {
            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            RaycastHit raycastHit;
            Ray directionRay = new Ray(origin, direction);

            if (Physics.SphereCast(directionRay, sphereCastRadius , out raycastHit, HookshotDistance))
            {
                //Hit something
                if (raycastHit.collider.CompareTag("HookHolder"))
                {
                    DebugHitPointTransform.position = raycastHit.point;
                    hookshotPosition = raycastHit.point;
                    hookshotSize = 0f;
                    hookshotTransform.gameObject.SetActive(true);
                    hookshotTransform.localScale = Vector3.zero;
                    state = State.HookshotThrown;
                    Player.PlayerState = CharController.State.LaunchHook;
                }

            }

        }
    }

    private void HookshotThrown()
    {
        hookshotTransform.LookAt(hookshotPosition);

        float hookshotThrowSpeed = 50f;
        hookshotSize += hookshotThrowSpeed * Time.deltaTime;
        hookshotTransform.localScale = new Vector3(1, 1, hookshotSize);

        if (hookshotSize >=Vector3.Distance(transform.position, hookshotPosition))
        {
            state = State.HookShotFlyingPlayer;
        }
 
    }

    private void HookShotMovement()
    {
        Player.PlayerState = CharController.State.OnHook;
        Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

        float hookshotSpeedMin = 25f;
        float hookshotSpeedMax = 40f;
        float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookshotPosition), hookshotSpeedMin, hookshotSpeedMax);
        float hookshotSpeedMultiplier = 2f;

        // Move Character Controller
        characterController.Move(hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

        float reachedHookshotPositionDistance = 1f;
        if (Vector3.Distance( transform.position, hookshotPosition) < reachedHookshotPositionDistance)
        {
            // Reached Hookshot Position
            state = State.Normal;
            Player.PlayerState = CharController.State.Idle;
            hookshotTransform.gameObject.SetActive(false);
        }   
    }
}
