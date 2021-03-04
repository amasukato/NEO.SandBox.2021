using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    //add value
    public bool GainHealing;
    public bool GainMana;

    public float Value = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HUD Player = other.gameObject.GetComponent<HUD>();

            if (GainHealing) Player.Heal(Value);
            if (GainMana) Player.GainMana(Value);

            Destroy(gameObject);
        }
    }


}
