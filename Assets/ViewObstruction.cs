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

                if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, PlayerTransform.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime);
                }
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, PlayerTransform.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * Time.deltaTime);
                }
            }
        }
    }
}
