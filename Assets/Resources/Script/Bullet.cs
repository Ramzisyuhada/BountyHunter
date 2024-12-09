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
    public  Role role;
    Camera cam;
    public void SetRole(int i)
    {
        
        role = (Role)i;
    }
    private void Update()
    {
        if(Physics.Linecast(Lastposition,Tip.position,out RaycastHit hit,layer))
        {
            if (role == Role.Player)
            {
                Debug.Log(hit.transform.root.gameObject.name);
                Debug.Log("Kena Hit");
                if (hit.transform.root.GetComponentInParent<Enemy_V>() != null) hit.transform.root.GetComponentInParent<Enemy_V>().Die();
            }
            else
            {
                Debug.Log("test");
                if (hit.transform.GetComponent<Player_V>() != null) {
                    hit.transform.GetComponent<Player_V>().shakeDuration = 0.3F;
                    hit.transform.GetComponent<Player_V>().player.TakeDamage(10);
                }
            }
        }
    }
}
