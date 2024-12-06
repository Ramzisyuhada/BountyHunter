using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet :MonoBehaviour
{
    private Vector3 Lastposition = Vector3.zero;
    public Transform Tip;

    private void Update()
    {
        if(Physics.Linecast(Lastposition,Tip.position,out RaycastHit hit))
        {
            
            if(hit.transform.gameObject.GetComponentInParent<Enemy_V>() != null) hit.transform.gameObject.GetComponentInParent<Enemy_V>().Die();
        }
    }
}
