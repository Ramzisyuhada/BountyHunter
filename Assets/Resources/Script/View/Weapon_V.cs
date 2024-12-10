using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Animator))]
public class Weapon_V : MonoBehaviour
{
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private LayerMask Mask;
    
    private Animator animator;
    private float LastShootTIme;
    /// <summary>
    /// Trigger Button
    /// </summary>
    [SerializeField] private InputActionProperty LeftTrigger;
    [SerializeField] private InputActionProperty RightTrigger;


    /// <summary>
    ///  Senjata
    /// </summary>
    /// 
    [SerializeField] private GameObject LeftController;
    [SerializeField] private GameObject RightController;

    [SerializeField] private GameObject raycast;
    [SerializeField] private GameObject Bullet;

    [SerializeField] private GameObject PrefabBullet;
    [SerializeField] public Weapon weapon;
    [SerializeField] private GameObject particle;

   // [SerializeField] private GameObject effect;
    public AudioSource audio;
    private bool Memegang;
    private XRDirectInteractor interactor;
    private GameObject ins;

    public enum Mode
    {
        VR,
        PC
    }
    public enum Role
    {
        Enemy,
        Player
    }
    public Mode ModeGame;

    public Role RoleGame;

    void Start()
    {
        if (RoleGame == Role.Player)
        {
            weapon = new Weapon(weapon.ammo, weapon.damage, weapon.FireRate, weapon.ShootDelay, weapon.LastShootTime);
            interactor = LeftController.GetComponent<XRDirectInteractor>();
            interactor.selectEntered.AddListener(OnSelectEntered);
            interactor.selectExited.AddListener(OnExitEntered);
        }
        else
        {
            audio.GetComponentInChildren<AudioSource>();
        }
    }
    
    private void OnExitEntered(SelectExitEventArgs e)
    {
        Memegang = false;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Memegang = true;
        audio = args.interactableObject.transform.GetComponentInChildren<AudioSource>();
        string name = args.interactableObject.transform.gameObject.name;
       
    }
    RaycastHit hit;

    private Vector3 Direct(Vector3 pos,Vector3 BulletSpreadVariance)
    {
        Vector3 direction = pos;
        BulletSpreadVariance += new Vector3 (Random.Range(-this.BulletSpreadVariance.x,this.BulletSpreadVariance.x), Random.Range(-this.BulletSpreadVariance.y, this.BulletSpreadVariance.y), Random.Range(-this.BulletSpreadVariance.z, this.BulletSpreadVariance.z));
        direction += BulletSpreadVariance;

        return direction;

    }
    public void Shoot(int i)
    {

       /* if (weapon.LastShootTime + weapon.ShootDelay < Time.time)
        {*/
            audio.Play();

            ShootingSystem.Play();
            Vector3 dir;
            if (RoleGame == Role.Player)
            {
                 dir = weapon.GetDirection(raycast.transform.forward, AddBulletSpread, BulletSpreadVariance);
            }
            else
            {
                Transform PlayerTransform = GameObject.FindWithTag("Player").transform;
                Vector3 target = PlayerTransform.position - raycast.transform.position;

                dir = Direct(raycast.transform.forward,target).normalized;
            }
            if (Physics.Raycast(raycast.transform.position, dir, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, raycast.transform.position, Quaternion.identity);
                trail.GetComponent<Bullet>().SetRole(i);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                weapon.LastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, raycast.transform.position, Quaternion.identity);
                trail.GetComponent<Bullet>().SetRole(i);

                StartCoroutine(SpawnTrail(trail, raycast.transform.position + dir * 100f, Vector3.zero, false));

                weapon.LastShootTime = Time.time;
            }


        /*}*/
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 hit,Vector3 normal, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, hit);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
          //  Trail.transform.position = Vector3.Lerp(startPosition, hit, time);

            Trail.transform.position = Vector3.Lerp(startPosition, hit, 1 - (remainingDistance / distance));

            remainingDistance -= 100f * Time.deltaTime;
            yield return null;  
        }
        //  animator.SetBool("IsShooting",false);
        Trail.transform.position = hit;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, hit, Quaternion.LookRotation(normal));
        }

        Destroy(Trail.gameObject, Trail.time);

    }


    void Update()
    {

        if (RoleGame == Role.Player)
        {
            if (Memegang)
            {

                Debug.DrawRay(raycast.transform.position, raycast.transform.forward * 10f, Color.red);


                if (ModeGame == Mode.VR)
                {
                    if (LeftTrigger.action.WasPressedThisFrame() || RightTrigger.action.WasPressedThisFrame())
                    {

                        Shoot(0);

                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Shoot(0);
                    }
                }
            }
        }
    
    }

  
}
