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
    public float resettime = 1f;

    public Collider[] attackHitboxes;
    public bool alreadyAttack, recovery;
    public CharController Player;

    private void Start()
    {
        Player = GetComponent<CharController>();
    }

    //Remove ExitTime on Animation2 ++
    void Update()
    {
        if(animatorWeapon.GetCurrentAnimatorStateInfo(0).IsName("Still 0"))
        {
            Player.PlayerState = CharController.State.IdleRecovery;
        }

        if (animatorWeapon.GetCurrentAnimatorStateInfo(0).IsName("Still"))
        {
            combonum = 0;
            reset = 0f;
            Player.PlayerState = CharController.State.Idle;
        }

        if (Input.GetButtonDown("Fire1") && combonum < 3 && (Player.PlayerState == CharController.State.Idle || Player.PlayerState == CharController.State.IdleRecovery))
        {
            animatorWeapon.SetTrigger(animlist[combonum]);
            //animatorPlayer.SetTrigger(animlist[combonum]);
            combonum++;
            reset = 0f;
            Player.PlayerState = CharController.State.Attacking;
        }

        reset += Time.deltaTime;
        if(reset > resettime)
        {
            animatorWeapon.SetTrigger("Reset");
            //animatorPlayer.SetTrigger("Reset");
            combonum = 0;


            Player.PlayerState = CharController.State.Idle;
        }
        
        if (combonum >= 3)
        {

            combonum = 0;
        }
    }
}
