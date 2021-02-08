using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public int[] table = 
    {
        60,
        30,
        10
    };

    public int total;
    public int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        foreach(var weight in table)
        {

            if (randomNumber <= weight)
            {

            }
            else
            {

            }
        }
    }

}
