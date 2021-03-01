using System.Collections;
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
        Player = GetComponent<CharController>();
    }


    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Left1"))
        {
            SubWeaponActive = true;
        } else if (Input.GetButtonUp("Left1"))
        {
            SubWeaponActive = false;
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
            if(SubWeaponActive == false)
            {
                WeaponAnim.Play("Standing Melee Attack 360 High 0"); //Standing Melee Attack 360 High 0
            }
            else if ( SubWeaponActive == true)
            {
                SubWeaponAnim.Play("chaine_2 0"); //Standing Melee Attack 360 High 0
                PlayerAnim.Play("Standing Melee Attack 360 High 0");
            }

        }
        if (comboStep >= 3)
        {
            if(SubWeaponActive == false)
            {
                WeaponAnim.Play("Sword And Shield Slash 0"); //Sword And Shield Slash 0
            }
            else if (SubWeaponActive == true)
            {
                SubWeaponAnim.Play("chaine_3 0"); //Standing Melee Attack 360 High 0
                PlayerAnim.Play("Sword And Shield Slash 0");
            }

        }
    }

    public void ComboReset()
    {
        comboPossible = false;
        comboStep = 0;
    }
}
