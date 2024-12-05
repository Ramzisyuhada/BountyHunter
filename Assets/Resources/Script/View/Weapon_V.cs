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
            ShootingSystem.Play();
            Vector3 dir = weapon.GetDirection(raycast.transform.forward,AddBulletSpread,BulletSpreadVariance);

            if (Physics.Raycast(raycast.transform.position, dir, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, raycast.transform.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                weapon.LastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, raycast.transform.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, raycast.transform.position + dir * 100f, Vector3.zero, false));

                weapon.LastShootTime = Time.time;
            }


        }
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

        
        if (Memegang)
        {

            Debug.DrawRay(raycast.transform.position, raycast.transform.forward * 10f, Color.red);



            if (LeftTrigger.action.WasPressedThisFrame() || RightTrigger.action.WasPressedThisFrame())
            {
                audio.Play();
                ins = Instantiate(PrefabBullet, Bullet.transform.position, Quaternion.identity);
                Destroy(ins, 2f);
                Shoot();
                
            }
        }
    }

  
}
