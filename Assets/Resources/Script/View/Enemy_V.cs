using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_V : MonoBehaviour
{
    public Enemy enemy;
  
    private Rigidbody[] rb;
    private Collider[] colliders;
    private Enemy_MV model;


    private void Start()
    {
       

        enemy = new Enemy(enemy.Health,enemy.Damage,enemy.Armor,enemy.IsBoss);
        setColiderState(true);
        setRigidbodyState(true);
       // model = new Enemy_MV();
    }


    public void Die()
    {
    //    GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColiderState(true);
    }


    public void setRigidbodyState(bool state)
    {
        rb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb1 in rb)
        {

            rb1.isKinematic = state;
        }
    }

    public void setColiderState(bool state)
    {
        colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = state;
        }
    }
}
