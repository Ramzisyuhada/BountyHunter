using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;


public class Bullet :MonoBehaviour
{
    private Vector3 Lastposition = Vector3.zero;
    public Transform Tip;
    public enum Role
    {
        Player,
        Enemy
    }

    public Role role;


    public void SetRole()
    {
        role = Role.Enemy;
    }
    private void Update()
    {
        if(Physics.Linecast(Lastposition,Tip.position,out RaycastHit hit))
        {
            if (role == Role.Player)
            {
                if (hit.transform.gameObject.GetComponentInParent<Enemy_V>() != null) hit.transform.gameObject.GetComponentInParent<Enemy_V>().Die();
            }
            else
            {
                Debug.Log("Kena hit");
            }
        }
    }
}
