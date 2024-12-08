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
    public LayerMask layer;
    public Role role;
    Camera cam;
    public void SetRole()
    {
        role = Role.Enemy;
    }
    private void Update()
    {
        if(Physics.Linecast(Lastposition,Tip.position,out RaycastHit hit,layer))
        {
            if (role == Role.Player)
            {
                if (hit.transform.gameObject.GetComponentInParent<Enemy_V>() != null) hit.transform.gameObject.GetComponentInParent<Enemy_V>().Die();
            }
            else
            {
                
                hit.transform.GetComponent<Player_V>().shakeDuration = 0.3F;
                hit.transform.GetComponent<Player_V>().player.TakeDamage(10);
            }
        }
    }
}
