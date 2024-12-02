using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFSM : FSM
{
    public enum FSMState
    {
        None,
        Patrol,
        Shoot,
        Die
    }


    public FSMState State;

    protected override void Initialize()
    {
        State = FSMState.Patrol;
        PostList = GameObject.FindGameObjectsWithTag("WayPoint");
        FindNextPoint();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = player.transform;
    }

    public void FSMPatrol(float speed,Transform pos)
    {
        State = FSMState.Patrol;
        PostList = GameObject.FindGameObjectsWithTag("WayPoint");
        FindNextPoint();
        Quaternion targetRoatation = Quaternion.LookRotation(DestPost - pos.position);
        transform.rotation = targetRoatation;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Debug.Log(targetRoatation);

        //transform.rotation = Quaternion.Slerp(transform.rotation,Target);
    }
    protected void FindNextPoint()
    {
        int randindex = Random.Range(0, PostList.Length);
        Vector3 rndPosition = Vector3.zero;
        DestPost = PostList[randindex].transform.position;
    }

    protected override void FSMUpdate()
    {
    }

    protected override void FSMFixedUpdate()
    {
    }
}
