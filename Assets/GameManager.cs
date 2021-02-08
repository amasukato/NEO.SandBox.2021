using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int countID = 0;
    
    [SerializeField] private GameObject Player;
    public Transform RespawnPoint;

    public Rigidbody PlayerSource;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if(Player == null)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        PlayerSource.transform.localScale = Vector3.one;
        PlayerSource.transform.position = RespawnPoint.transform.position;
        PlayerSource.velocity = Vector3.zero;
    }
}
