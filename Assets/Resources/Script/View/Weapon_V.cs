using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon_V : MonoBehaviour
{

  
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

        weapon = new Weapon(weapon.ammo,weapon.damage,weapon.FireRate);
        interactor = LeftController.GetComponent<XRDirectInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Memegang = true;
        audio = args.interactableObject.transform.GetComponentInChildren<AudioSource>();
        string name = args.interactableObject.transform.gameObject.name;
       
    }
    RaycastHit hit;

    void Update()
    {
        Debug.DrawRay(raycast.transform.position, raycast.transform.forward * 10f, Color.red);
        if (Memegang)
        {
            if (LeftTrigger.action.WasPressedThisFrame() || RightTrigger.action.WasPressedThisFrame())
            {

                if (Time.time > weapon.FireRate)
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
                }
            }
        }
    }

  
}
