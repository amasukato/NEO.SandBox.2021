using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Animator WeaponAnim;
    public Animator SubWeaponAnim;
    public Animator PlayerAnim;

    [SerializeField] private bool SubWeaponActive;
    [SerializeField] private bool comboPossible;
    [SerializeField] private int comboStep;

    public Collider[] attackHitboxes;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Left1"))
        {
            SubWeaponActive = true;
        } else
        {
            SubWeaponActive = false;
        }
    }
    public void Attack()
    {

        if(comboStep == 0)
        {

            WeaponAnim.Play("Standing 1H Magic Attack 01 0");
            comboStep = 1;
            return;

        }

        if(comboStep != 0)
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

            WeaponAnim.Play("Standing Melee Attack 360 High 0");



        }
        if (comboStep >= 3)
        {
            WeaponAnim.Play("Sword And Shield Slash 0");

        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }
}
