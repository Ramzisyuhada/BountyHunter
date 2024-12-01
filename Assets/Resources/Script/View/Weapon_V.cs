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
   // [SerializeField] private GameObject effect;
    private AudioSource audio;
    private bool Memegang;
    private XRDirectInteractor interactor;
    private GameObject ins;



    

    void Start()
    {
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
                ins = Instantiate(PrefabBullet, Bullet.transform.position, Quaternion.identity);
                audio.Play();
             //   GameObject pref = Instantiate(effect,raycast.transform.position, effect.transform.rotation);
              //  Destroy(pref,0.1f);
                if (Physics.Raycast(raycast.transform.position, raycast.transform.forward, out hit))
                {
                    Debug.Log("Ray hit object: " + hit.transform.gameObject.name);
                }
                else
                {
                    Debug.Log("Ray did not hit anything.");
                }
            }
        }
    }

  
}
