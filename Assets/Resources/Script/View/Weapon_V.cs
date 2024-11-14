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
    private bool Memegang;
    private XRDirectInteractor interactor;

    void Start()
    {
        interactor = LeftController.GetComponent<XRDirectInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Memegang = true;
      
    }
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(raycast.transform.position, -raycast.transform.right * 10f, Color.red);
        if (Memegang)
        {
            if (LeftTrigger.action.WasPressedThisFrame() || RightTrigger.action.WasPressedThisFrame())
            {

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
