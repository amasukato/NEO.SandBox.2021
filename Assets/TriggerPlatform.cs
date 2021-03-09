using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    public Animator TriggerAnim;
    public Animator DoorAnim;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("MobileObject"))
        {
            TriggerAnim.SetBool("SwitchOn", true);
            DoorAnim.SetBool("IsOpening", true);
            FindObjectOfType<AudioManager>().Play("DoorOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("MobileObject"))
        {
            TriggerAnim.SetBool("SwitchOn", false);
            DoorAnim.SetBool("IsOpening", false);
            FindObjectOfType<AudioManager>().Play("DoorClose");
        }
    }

}
