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

    Weapon_V weapon;
    public enum FSMState
    {
        None,
        Patrol,
        Shoot,
        Die
    }


    public FSMState State;

    public void Diffculty(float firerate)
    {
        enemy.FireRate = firerate;
    }
    protected override void Initialize()
    {
        Animator anim = GetComponent<Animator>();

        // AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        weapon = GetComponentInChildren<Weapon_V>();
        //clips[1].speed = 1;
        anim.SetFloat("SpeeShoot", 3);
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
        

        if (Vector3.Distance(transform.position, DestPost) < 5f )
        {

            // Setiap Sampai Destination dia akan  keadan Shoot
            
            Debug.Log("Menembak Player");

            isWaiting = false;

            State = FSMState.Shoot;
            return;


        }
        Quaternion targetRoatation = Quaternion.LookRotation(DestPost - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRoatation, Time.deltaTime * enemy.rotationSpeed);
        GetComponent<Animator>().SetFloat("Walk", enemy.Speed);

        /*transform.Translate(Vector3.forward * Time.deltaTime * enemy.Speed);*/

    }


    /// <summary>
    /// Ketika Player sudah melihat ke player otomatis 
    /// </summary>
    /// 
    void FSMShoot()
    {
        Vector3 target = PlayerTransform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * enemy.rotationSpeed);
        if (Vector3.Angle(transform.forward, target) < 20)
        {
          GetComponent<Animator>().SetFloat("Walk", 0f);


            

              /*  if (!isWaiting)
                {
                    Debug.Log("Nembak");

                    FindNextPoint();

                }
                StartCoroutine(StartDestination());*/


            
        }
       

        

    }

    void TriggerEvent()
    {
        Vector3 target = PlayerTransform.position - transform.position;

        weapon.Shoot(1);
        enemy.Shoot(transform.position, target);
    }


    bool isWaiting = false;
    IEnumerator StartDestination()
    {
        isWaiting = true;


        yield return new WaitForSeconds(7f);

        State = FSMState.Patrol;




    }
    protected void FindNextPoint()
    {
        
        int randindex = Random.Range(0, PostList.Length);
        Vector3 rndPosition = Vector3.zero;
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

        Destroy(gameObject,0.5f);
    }


    public void setRigidbodyState(bool state)
    {
        rb = GetComponentsInChildren<Rigidbody>();
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
