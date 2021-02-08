using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform[] EnemyPos;

    public float repeatRate = 2f;
    public float LifeTime = 10f;

    public bool OnDeathSpawn, RandomSpawnPoint, RandomEnemies;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter ( Collider other)
    {
        InvokeRepeating("EnemySpawner", 0.5f, repeatRate);
        Destroy(gameObject, LifeTime);
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    private void OnEnable()
    {
        if (OnDeathSpawn == true)
        {
            EnemyAI.OnEnemyKilled += EnemySpawner;
        }

    }

    void EnemySpawner()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0f, EnemyPos.Length - 1));

        Instantiate(enemyPrefab, EnemyPos[randomNumber].position, EnemyPos[randomNumber].rotation);
    }

    void OnDrawGizmos()
    {
     Gizmos.color = Color.red;

        foreach (Collider col in GetComponents<Collider>())
        {
            Gizmos.matrix = col.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }

    }
}
