﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text healthText;
    public Image healthBar;

    public Image ManaBar;
    public RawImage AnimatedManaBar;
    public RectTransform barMaskRectTransform;
    private float barMaskWidth;

    public CharController charController;

    public CameraController cameraShake;

    float health; 
    float maxHealth;

    float mana;
    float maxMana;

    float lerpSpeed;

    void Start()
    {
        health = GetComponent<CharController>().HitPoints;
        maxHealth = GetComponent<CharController>().MaxHitPoints;

        mana = GetComponent<CharController>().ManaPoints;
        maxMana = GetComponent<CharController>().MaxManaPoints;
        mana = maxMana;

        barMaskWidth = barMaskRectTransform.sizeDelta.x;

        charController = GetComponent<CharController>();

    }


    void Update()
    {
        healthText.text = "HP : " + health;
        if (health > maxHealth) health = maxHealth;
        if (mana > maxMana) mana = maxMana;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFilter();
        ColorChanger();

        ManaBarFilter();
        //AnimatedManaBarFiller();
    }

    void HealthBarFilter()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);


    }

    void ManaBarFilter()
    {
        ManaBar.fillAmount = Mathf.Lerp(ManaBar.fillAmount, mana / maxMana, lerpSpeed);
    }

    void AnimatedManaBarFiller()
    {
        Rect uvRect = AnimatedManaBar.uvRect;
        uvRect.x += 0.5f * Time.deltaTime;
        AnimatedManaBar.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = GetManaNormalized() * barMaskWidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;
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
            //charController.HitPoints -= damagePoints;

        }
    }

    public void Heal (float healingPoints)
    {
        if (health < maxHealth)
        {
            health += healingPoints;
            //charController.HitPoints += healingPoints;
        }
    }

    public void GainMana(float gainMana)
    {
        if (mana < maxMana)
        {
            mana += gainMana;
            charController.ManaPoints += gainMana;
        }

    }

    public void SpendMana(float spendMana)
    {
        if (mana >= spendMana)
        {
            mana -= spendMana;
            charController.ManaPoints -= spendMana;
        }

    }

    public float GetManaNormalized()
    {
        return mana / maxMana;
    }

}


