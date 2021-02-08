using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{

    public float health = 10;

    public Faction faction = Faction.Enemy;
    public Faction[] enemies;
    public int id;
    public Damager weapon;
    public float maxBlock; // change to Dash
    public DefenseState dState = DefenseState.Open;
    public Status status = Status.Normal;
    public float knockbackForce = 5f;
    public float knockbackResist = 0f;


    public delegate void CombatEvent(Combatant attacker, Combatant defender);
    public CombatEvent TakeDamage;

    private void Start()
    {
        GameManager.countID++;
        id = GameManager.countID;
        TakeDamage += Damaged;
    }

    private void Damaged(Combatant attacker, Combatant defender)
    {
        health -= 1;
    }


    //Animation Events
    public void StartAttack()
    {
        weapon.ActivateDamager();
    }
    public void EndAttack()
    {
        weapon.DeactivateDamager();
    }

}

public enum Faction { Player, Enemy };
public enum DefenseState { Open, Blocking};
public enum Status { Normal, Knockback };
