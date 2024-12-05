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
    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject particle;

   // [SerializeField] private GameObject effect;
    private AudioSource audio;
    private bool Memegang;
    private XRDirectInteractor interactor;
    private GameObject ins;



    

    void Start()
    {

        weapon = new Weapon(weapon.ammo,weapon.damage,weapon.FireRate,weapon.ShootDelay,weapon.LastShootTime);
        interactor = LeftController.GetComponent<XRDirectInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnExitEntered);
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
    private void Shoot()
    {

        if (weapon.LastShootTime + weapon.ShootDelay < Time.time)
        {
            animator.SetBool("IsShooting", true);
            ShootingSystem.Play();
            Vector3 dir = weapon.GetDirection(transform.forward,AddBulletSpread,BulletSpreadVariance);
            if (Physics.Raycast(raycast.transform.position, dir, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, raycast.transform.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                LastShootTIme = Time.time;
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;
        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time = Time.deltaTime / Trail.time;
            yield return null;  
        }
        animator.SetBool("IsShooting",false);
        Trail.transform.position = hit.point;
        Instantiate(ImpactParticleSystem,hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(Trail.gameObject,Trail.time);
    }


    void Update()
    {

        
        Debug.DrawRay(raycast.transform.position, raycast.transform.forward * 10f, Color.red);
        if (Memegang)
        {
            if (LeftTrigger.action.WasPressedThisFrame() || RightTrigger.action.WasPressedThisFrame())
            {
                Shoot();
             
               // weapon.Shoot()
                //weapon.Shoot(weapon.LastShootTime,weapon.ShootDelay);

               /* if (Time.time > weapon.FireRate)
                {
                    ins = Instantiate(PrefabBullet, Bullet.transform.position, Quaternion.identity);
                    audio.Play();
                    weapon.FireRate = Time.time + 0.5f;
                    if (Physics.Raycast(raycast.transform.position, raycast.transform.forward, out hit))
                    {
                        Enemy_V enemy = hit.transform.GetComponentInParent<Enemy_V>();
                        if (hit.transform.gameObject.layer == 8) { 
                            enemy.Die();
                          //  GameObject darah = Instantiate(particle, hit.transform.gameObject.transform.position, Quaternion.identity);
                            Debug.Log(enemy.enemy.Health);
                        }
                    }
                    else
                    {
                        Debug.Log("Ray did not hit anything.");
                    }
                }*/
            }
        }
    }

  
}
