﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Animator WeaponAnim;
    public Animator SubWeaponAnim;
    public Animator PlayerAnim;

    public CharController Player;


    [SerializeField] private bool SubWeaponActive;
    [SerializeField] private bool comboPossible;
    [SerializeField] private int comboStep;

    public Collider[] attackHitboxes;


    public void Start()
    {
        Player = GetComponentInParent<CharController>();

    }


    public void Update()
    {
        if (Input.GetButtonDown("Fire1") &&
           (Player.PlayerState == CharController.State.Idle || Player.PlayerState == CharController.State.IdleRecovery || 
            Player.PlayerState == CharController.State.Moving || Player.PlayerState == CharController.State.Attacking)) //
        {
            Attack();
            Player.PlayerState = CharController.State.Attacking;
        }

        if (Input.GetButtonDown("Left1") )
        {
            if (!SubWeaponActive)
            {
                SubWeaponActive = true;
            }
            else
            {
                SubWeaponActive = false;
            }
        }

    }
    public void Attack()
    {

        if(comboStep == 0)
        {
            if (SubWeaponActive == false)
            {
                WeaponAnim.Play("Standing 1H Magic Attack 01 0");
                comboStep = 1;
                return;
            }
            else if (SubWeaponActive == true)
            {
                SubWeaponAnim.Play("chaine_1 0");
                PlayerAnim.Play("Standing 1H Magic Attack 01 0");
                comboStep = 1;
                return;
            }

        }

        else if(comboStep != 0)
        {
            if(comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }

    public void Combo()
    {
        if (comboStep >= 2)
        {
            if(SubWeaponActive == false)
            {
                WeaponAnim.Play("Standing Melee Attack 360 High 0"); 
            }
            else if ( SubWeaponActive == true)
            {
                SubWeaponAnim.Play("chaine_2 0");
                PlayerAnim.Play("Standing Melee Attack 360 High 0");
            }
            Player.PlayerState = CharController.State.IdleRecovery;

        }
        if (comboStep >= 3)
        {
            if(SubWeaponActive == false)
            {
                WeaponAnim.Play("Sword And Shield Slash 0");
            }
            else if (SubWeaponActive == true)
            {
                SubWeaponAnim.Play("chaine_3 0");
                PlayerAnim.Play("Sword And Shield Slash 0");
            }
            Player.PlayerState = CharController.State.IdleRecovery;
        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
        Player.PlayerState = CharController.State.Idle;
    }

}
