using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public Combatant owner;
    public int[] hitEnemies = new int[10];
    public const int NOTHING = -1;
    public bool active = false;


    private void Start()
    {
        EmptyHitList();
    }

    private void EmptyHitList()
    {
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i] = NOTHING;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Controlled by Animation Events
        if (active)
        {
            //Is collider onwer a Combatant
            if (other.gameObject.GetComponent<Combatant>() != null)
            {
                //Check the Other Combatants faction against our list of enemy faction
                for (int i = 0; i < owner.enemies.Length; i++)
                {
                    //This is a enemy
                    if (owner.enemies[i] == other.GetComponent<Combatant>().faction)
                    {

                        //Checks to see if we have already hit this enemy
                        for (int ii = 0; ii < hitEnemies.Length; ii++)
                        {
                            //Damage the enemy
                            if (hitEnemies[ii] == NOTHING)
                            {
                                Combatant defender = other.gameObject.GetComponent<Combatant>();
                                CalculateCombat(owner, defender);
                                hitEnemies[ii] = other.GetComponent<Combatant>().id;
                                break;

                            }
                            //Already damage so abandon loop
                            else if (hitEnemies[ii] == other.GetComponent<Combatant>().id)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

    }

    public void CalculateCombat (Combatant attacker, Combatant defender)
    {
        if (defender.dState == DefenseState.Open)
        {
            // attacker damagee defender
            DealDamage(attacker, defender);
        }
        else if (defender.dState == DefenseState.Blocking)
        {
            //test to see block angle
            float attackAngle = 100f - Vector3.Angle(attacker.transform.forward, defender.transform.forward);
            if(attackAngle <= defender.maxBlock)
            {
                // the defender block
            }
            else
            {
                // attacker damages defender
                DealDamage(attacker, defender);
            }
        }
    }

    public void DealDamage(Combatant attacker, Combatant defender)
    {
        defender.TakeDamage(attacker, defender);
    }

    //Method for Animation Events
    public void ActivateDamager()
    {
        active = true;
        EmptyHitList();
    }

    public void DeactivateDamager()
    {
        active = false;
    }
}
