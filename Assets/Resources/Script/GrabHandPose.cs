using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class GrabHandPose : MonoBehaviour
{

    public HandData leftHandPose;
    public GameObject LeftPhysic;
    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;
    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotation;
    private Quaternion[] finalFingerRotation;


    
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnsetPoses);
        leftHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs args)
    {
        //HandData pose = leftHandPose.GetComponent<HandPresentPhysic>().target.transform.GetComponentInChildren<HandData>();

        if (args.interactorObject is XRDirectInteractor rayInteractor)
        {
                HandData   handData = rayInteractor.transform.GetComponentInChildren<HandData>();
               

                LeftPhysic.transform.GetComponentInChildren<HandData>().animator.enabled = false;


            SetHandDataValue(LeftPhysic.transform.GetComponentInChildren<HandData>(), leftHandPose);
                SetHandData(LeftPhysic.transform.GetComponentInChildren<HandData>(), finalHandPosition, finalHandRotation, finalFingerRotation);
        }
    }

    public void UnsetPoses(BaseInteractionEventArgs args)
    {

        if (args.interactorObject is XRDirectInteractor rayInteractor)
        {
            // LeftPhysic.SetActive(true);

            LeftPhysic.transform.GetComponentInChildren<HandData>().animator.enabled = false;
            SetHandData(LeftPhysic.transform.GetComponentInChildren<HandData>(), startingHandPosition, startingHandRotation, startingFingerRotation);

        }
    }

    public void SetHandDataValue(HandData h1, HandData h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotation = new Quaternion[h1.FingerOne.Length];
        finalFingerRotation = new Quaternion[h2.FingerOne.Length]; 

        for (int i = 0; i < h1.FingerOne.Length; i++)
        {
            startingFingerRotation[i] = h1.FingerOne[i].localRotation;
            finalFingerRotation[i] = h2.FingerOne[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.FingerOne[i].localRotation = newBonesRotation[i];
        }
    }
    
}
