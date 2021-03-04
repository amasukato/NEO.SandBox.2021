using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointBar : MonoBehaviour
{

    private SkillPoint skillpoints;
    public Image barImage;

    private void Awake()
    {

        skillpoints = new SkillPoint();
    }

    private void Update()
    {
        skillpoints.Update();

        barImage.fillAmount = skillpoints.GetSkillNormalized();
    }
}

public class SkillPoint
{
    public const int SKILL_MAX = 100;

    private float skillAmount;
    private float skillRegenAmount;

    public void Skill()
    {
        skillAmount = 0;
        skillRegenAmount = 30f;
    }

    public void Update()
    {
        skillAmount += skillRegenAmount * Time.deltaTime;
        skillAmount = Mathf.Clamp(skillAmount, 0f, SKILL_MAX);

    }

    public void SpendSkillPoints(int amount)
    {
        if (skillAmount >= amount)
        {
            skillAmount -= amount;
        }
    }

    public float GetSkillNormalized()
    {
        return skillAmount / SKILL_MAX;
    }
}
