using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon 
{

    [field:SerializeField]
    public int ammo;
    [field: SerializeField]
    public int damage;


    [field: SerializeField]
    public float FireRate;
    public Weapon(int ammo, int damage, float fireRate)
    {
        this.ammo = ammo;
        this.damage = damage;
        FireRate = fireRate;
    }
}
