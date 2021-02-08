using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    List<string> animlist = new List<string>(new string[] { "animation1", "animation2", "animation3" });
    public Animator animator;
    public int combonum;
    public float reset;
    public float resettime;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && combonum < 3)
        {
            animator.SetTrigger(animlist[combonum]);
            combonum++;
            reset = 0f;

        }
        if (combonum > 0 )
        {
            reset += Time.deltaTime;
            if(reset > resettime)
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
}
