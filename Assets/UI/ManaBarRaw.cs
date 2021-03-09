using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBarRaw : MonoBehaviour {

    private Mana mana;
    //private Image ManaBar;
    private RawImage RawManaBar;


    private void Awake()
    {
        //ManaBar = transform.Find("RawManaBar").GetComponent<Image>();
        RawManaBar = transform.Find("RawManaBar").GetComponent<RawImage>();

        mana = new Mana();

    }

    private void Update()
    {
        mana.Update();

        //ManaBar.fillAmount = mana.GetManaNormalized();
        Rect uvRect = RawManaBar.uvRect;
        uvRect.x -= .5f * Time.deltaTime;
        RawManaBar.uvRect = uvRect;
    }

    
}

public class Mana
{
    public const int MANA_MAX = 100;

    private float manaAmount;
    private float manaRegenAmount;

    public Mana()
    {
        manaAmount = 0;
        manaRegenAmount = 30f;
    }

    public void Update()
    {
        manaAmount += manaRegenAmount * Time.deltaTime;
    }

    public void TrySpendMana (int amount)
    {
        if (manaAmount >= amount)
        {
            manaAmount -= amount;
        }
    }

    public float GetManaNormalized()
    {
        return manaAmount / MANA_MAX;
    }
}