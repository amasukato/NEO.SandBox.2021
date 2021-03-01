using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Text healthText;
    public Image healthBar;

    float health; 
    float maxHealth;
    float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<CharController>().HitPoints;
        maxHealth = GetComponent<CharController>().MaxHitPoints;
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP " + health;
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFilter();
        ColorChanger();
    }

    void HealthBarFilter()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);


    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

        healthBar.color = healthColor;
    }

    public void Damage (float damagePoints)
    {
        if (health > 0 )
        {
            health -= damagePoints;
        }
    }

    public void Heal (float healingPoints)
    {
        if (health < maxHealth)
        {
            health += healingPoints;
        }
    }
}
