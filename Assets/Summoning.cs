using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform[] EnemyPos;
    // Start is called before the first frame update

    private void Summon()
    {
        int randomNum = Mathf.RoundToInt(Random.Range(0f, EnemyPos.Length - 1));

        Instantiate(EnemyPrefab, EnemyPos[randomNum].position, EnemyPos[randomNum].rotation);

    }

}
