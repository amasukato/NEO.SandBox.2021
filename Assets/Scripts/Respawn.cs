using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform RespawnPoint;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Respawning();
        } else
        {
            Destroy(other.gameObject);
        }
    }

    public void Respawning()
    {
        Player.transform.position = RespawnPoint.transform.position;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        foreach (Collider col in GetComponents<Collider>())
        {
            Gizmos.matrix = col.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}
