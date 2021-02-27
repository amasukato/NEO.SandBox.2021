using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewObstruction : MonoBehaviour
{
    public Transform PlayerTransform, Target;
    public Transform Obstruction;


    void ViewObstructed()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f)) ;
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

            }
        }
    }
}
