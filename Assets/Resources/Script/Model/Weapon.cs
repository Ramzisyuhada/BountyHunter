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
    [field: SerializeField]
    public float ShootDelay;
    [field:SerializeField]
    public float LastShootTime;
    
    public Weapon(int ammo, int damage, float fireRate, float shootDelay, float lastShootTime)
    {
        this.ammo = ammo;
        this.damage = damage;
        FireRate = fireRate;
        ShootDelay = shootDelay;
        LastShootTime = lastShootTime;
    }

    public Vector3 GetDirection(Vector3 pos,bool AddBulletSpread,Vector3 BulletSpreadVariance)
    {
        Vector3 direction = pos;

        if (AddBulletSpread)
        {
            direction +=new Vector3(Random.Range(-BulletSpreadVariance.x,BulletSpreadVariance.x),Random.Range(-BulletSpreadVariance.y,BulletSpreadVariance.y),Random.Range(-BulletSpreadVariance.z,BulletSpreadVariance.z));
            direction.Normalize();
        }
        return direction;
       
       
    }
}
