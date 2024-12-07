using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{


    [SerializeField] private GameObject PrefabEnemy;

    private int EnemyCount;

    GameObject[] SpawnPoint;
    void Start()
    {
        SpawnPoint = GameObject.FindGameObjectsWithTag("Spawn");
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
        {
            Spawn();
        }
        

        
    }

    private void Spawn()
    {
        int index = Random.Range(0, SpawnPoint.Length);
        Instantiate(PrefabEnemy, SpawnPoint[index].transform.position,Quaternion.identity);
    }
}
