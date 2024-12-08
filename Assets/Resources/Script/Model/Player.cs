using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class Player {

    [field: SerializeField] public float health;
    [field: SerializeField] public float score;

    public Player(float health)
    {
        this.health = health;
        this.score = 0;
    }


    public void TakeDamage(int damageamout)
    {
       

       
        if (health <= 0) {
            Die();
        }
        else
        {
            health -= damageamout;

        }
    }

    private void Die() { 
    
    
    }
}
