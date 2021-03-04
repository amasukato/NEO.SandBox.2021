using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    List<string> animlist = new List<string>(new string[] { "Attack", "Attack2", "Attack3" });
    public Animator animator;

    //Stats
    public float damage;

    //Combo Setting
    public int combonum;
    public float reset;
    public float resettime;


    //Remove ExitTime on Animation2 ++
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && combonum < 3)
        {
            animator.SetTrigger(animlist[combonum]);
            combonum++;
            reset = 0f;

        }
        if (combonum > 0)
        {
            reset += Time.deltaTime;
            if (reset > resettime)
            {
                animator.SetTrigger("Reset");
                combonum = 0;
            }
        }
        if (combonum == 3)
        {
            resettime = 3f;
            combonum = 0;
        }
        else
        {
            resettime = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HUD health = other.GetComponentInParent<HUD>();

        if ( health !=null)
        {
            health.Damage(damage);
        }
    }
}
