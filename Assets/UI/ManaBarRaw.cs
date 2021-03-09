using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBarRaw : MonoBehaviour {
    
    private RawImage RawManaBar;

    private void Awake()
    {
        RawManaBar = transform.Find("RawManaBar").GetComponent<RawImage>();

    }


    



   
    
}
