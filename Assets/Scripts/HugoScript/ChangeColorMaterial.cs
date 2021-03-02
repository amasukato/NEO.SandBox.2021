using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorMaterial : MonoBehaviour
{

    public Material[] material;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "Player")
        {
            rend.sharedMaterial = material[1];

        }
        else
        {
            rend.sharedMaterial = material[0];
        }

    }
}
