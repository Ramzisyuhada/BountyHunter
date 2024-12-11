using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEnemy : MonoBehaviour
{

    private float start = 60f;
    private float currrent;
    [SerializeField] private GameObject PrefabEnemy;


    [Header("GUI")]
    [SerializeField] private Text Waktu;
    private int EnemyCount;
    private bool mulai =false;
    GameObject[] SpawnPoint;
    void Start()
    {
        currrent = start;
        SpawnPoint = GameObject.FindGameObjectsWithTag("Spawn");
       // Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (currrent > 0 && mulai)
        {
            currrent -= Time.deltaTime; 

            int minutes = Mathf.FloorToInt(currrent / 60); 
            int seconds = Mathf.FloorToInt(currrent % 60); 

            Waktu.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }
        else
        {
            Destroy(GameObject.FindWithTag("Enemy"));
            mulai = false;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1 && mulai)
        {
            Spawn();
        }
        

        
    }
    public void Diffculty(float firerate)
    {
        mulai = true;
        PrefabEnemy.GetComponent<Enemy_V>().enemy.FireRate = firerate;
    }
    private void Spawn()
    {
        int index = Random.Range(0, SpawnPoint.Length);
        Instantiate(PrefabEnemy, SpawnPoint[index].transform.position,Quaternion.identity);
    }
}
