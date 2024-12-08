using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public class Enemy
{

    [field: SerializeField]
    public float Health { get; private set; }
    [field: SerializeField]
    public float Damage { get; private set; }
    [field: SerializeField]
    public float Armor { get; private set; }
    [field: SerializeField]
    public bool IsBoss { get; private set; }

    [field: SerializeField]
    public float Speed {  get; private set; }

    [field : SerializeField]
    public float rotationSpeed {  get; private set; }


    [field: SerializeField]
    public float FireRate;
    public Enemy(float health, float damage, float armor, bool isBoss, float speed, float rotationSpeed, float firerate)
    {
        Health = health;
        Damage = damage;
        Armor = armor;
        IsBoss = isBoss;
        Speed = speed;
        this.rotationSpeed = rotationSpeed;
        FireRate = firerate;
    }


    public void GetDamage(int damage)
    {


        if (this.Health > 0)
        {
            this.Health -= damage;

        }
       
    }

    public void Shoot(Vector3 positionEnemy,Vector3 target) {
        Debug.Log(Vector3.Angle(positionEnemy, target));

      

            if (Physics.Raycast(positionEnemy, target, out RaycastHit hit))
            {
                //Debug.Log(hit.point);
            }
        
    }

}
