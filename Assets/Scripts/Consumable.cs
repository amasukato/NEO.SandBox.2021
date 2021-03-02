using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    //add value
    public float healingValue = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health Player = other.gameObject.GetComponent<Health>();
            Player.Heal(healingValue);
            Destroy(gameObject);
        }
    }


}
