using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Animator WeaponAnim;
    bool comboPossible;
    int comboStep;


    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
    public void Attack()
    {

        if(comboStep == 0)
        {
            WeaponAnim.Play("AttackA");
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
        if (comboStep == 2)
        {
            WeaponAnim.Play("AttackB");

        }
        if (comboStep == 3)
        {
            WeaponAnim.Play("AttackC");

        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }
}
