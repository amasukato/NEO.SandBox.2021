using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : EnemyAI
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
