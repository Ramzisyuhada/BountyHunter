using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy 
{

    [field:SerializeField]
    public float Health { get; private set; }
    [field:SerializeField]
    public float Damage { get; private set; }
    [field:SerializeField]
    public float Armor { get; private set; }
    [field: SerializeField]

    public bool IsBoss { get; private set; }

    public Enemy(float health, float damage, float armor, bool isBoss)
    {
        Health = health;
        Damage = damage;
        Armor = armor;
        IsBoss = isBoss;
    }


    public void GetDamage(int damage)
    {
        if (this.Health > 0)
        {
            this.Health -= damage;

        }
       
    }

}
