using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SimpleFSM;
using static UnityEngine.GraphicsBuffer;

public class Enemy_V : FSM
{
    public Enemy enemy;
    private SimpleFSM fSM;
    private Rigidbody[] rb;
    private Collider[] colliders;
    private Enemy_MV model;


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
        
        enemy = new Enemy(enemy.Health, enemy.Damage, enemy.Armor, enemy.IsBoss, enemy.Speed, enemy.rotationSpeed,enemy.FireRate);
        fSM = new SimpleFSM();
        setColiderState(true);
        setRigidbodyState(true);

        State = FSMState.Patrol;
        PostList = GameObject.FindGameObjectsWithTag("WayPoint");
        FindNextPoint();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = player.transform;
    }

    public void FSMPatrol()
    {
        

        if (Vector3.Distance(transform.position, DestPost) < 0.1f )
        {

            // Setiap Sampai Destination dia akan  keadan Shoot
            Debug.Log("Menembak Player");
                            GetComponent<Animator>().SetTrigger("Shoot");

            State = FSMState.Shoot;
            return;

           FindNextPoint();

        }
        Quaternion targetRoatation = Quaternion.LookRotation(DestPost - transform.position);
        transform.rotation = Quaternion.Lerp (transform.rotation,targetRoatation ,Time.deltaTime * enemy.rotationSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * enemy.Speed);

    }


    /// <summary>
    /// Ketika Player sudah melihat ke player otomatis 
    /// </summary>
    void FSMShoot()
    {
        Vector3 target = PlayerTransform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * enemy.rotationSpeed);
        if (Vector3.Angle(transform.position, target.normalized) <= 180) { 

            if (Time.time > enemy.FireRate)
            {
                
                enemy.Shoot(transform.position, target);

                enemy.FireRate = Time.time + 1.5f;

                StartCoroutine(StartDestination());

            }

        }

    }

    bool isWaiting = false;
    IEnumerator StartDestination()
    {
        isWaiting = true;


        yield return new WaitForSeconds(7f);
        Debug.Log("Hello world");

        GetComponent<Animator>().SetTrigger("Walk");


        State = FSMState.Patrol;
        FindNextPoint();

    }
    protected void FindNextPoint()
    {
        
        int randindex = Random.Range(0, PostList.Length);
        Vector3 rndPosition = Vector3.zero;
        Debug.Log(PostList[randindex].gameObject.name);
        DestPost = PostList[randindex].transform.position;
    }

    protected override void FSMUpdate()
    {

        switch (State)
        {
            case FSMState.Patrol : FSMPatrol();break;
            case FSMState.Shoot : FSMShoot();break;
        }

    }
    
    protected override void FSMFixedUpdate()
    {
    }
   
    public void Die()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColiderState(true);
    }


    public void setRigidbodyState(bool state)
    {
        rb = GetComponentsInChildren<Rigidbody>();
        Debug.Log(rb.Length);
        foreach (Rigidbody rb1 in rb)
        {

            rb1.isKinematic = state;
        }
    }

    public void setColiderState(bool state)
    {
        colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = state;
        }
    }
}
