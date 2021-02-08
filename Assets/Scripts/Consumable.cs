using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public float AttractSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, AttractSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 1 )
        {
            Destroy(gameObject);
        }
    }
}
