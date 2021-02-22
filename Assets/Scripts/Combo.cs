using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    List<string> animlist = new List<string>(new string[] { "Attack", "Attack2", "Attack3" });
    public Animator animatorWeapon;
    public Animator animatorPlayer;
    public int combonum;
    public float reset;
    public float resettime;

    public Collider[] attackHitboxes;
    public bool alreadyAttack, recovery;


    //Remove ExitTime on Animation2 ++
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && combonum < 3)
        {
            animatorWeapon.SetTrigger(animlist[combonum]);
            //animatorPlayer.SetTrigger(animlist[combonum]);
            combonum++;
            reset = 0f;

        }

            if (combonum > 0 )
        {
            reset += Time.deltaTime;
            if(reset > resettime)
            {
                animatorWeapon.SetTrigger("Reset");
                //animatorPlayer.SetTrigger("Reset");
                combonum = 0;
            }
        }
        if (combonum == 3)
        {
            resettime = 2f;
            combonum = 0;
        }
        else
        {
            resettime = 1f;
            
        }
    }
}
