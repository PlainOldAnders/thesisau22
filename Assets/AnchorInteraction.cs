using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnchorInteraction : MonoBehaviour
{

    [SerializeField]
    private OVRHand ovrHand;


    [SerializeField]
    private OVRHand.Hand HandType = OVRHand.Hand.None;
    [SerializeField]
    private int maxFrames = 60;

    bool isIndexFingerPinching = false;
    bool isMiddleFingerPinching = false;

    bool isPinkyFingerPinching = false;
    private int spatialAnchorCount = 0; 

    private void OnEnable()
    {
        if (ovrHand == null)
        {
            Debug.Log("ovrHand was set faultly in the inspector");
        }
        else
        {
            Debug.Log("ovrHand was set correctly in the inspector");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadAnchors();
    }

    // Update is called once per frame
    void Update()
    {
        spatialAnchorCount = AnchorSession.Instance.handleToAnchor.Count; 
        registerInteraction();

    }


    private void registerInteraction()
    {

        if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && !isIndexFingerPinching)
        {
            isIndexFingerPinching = true;
            PlaceAnchor();

        }

        if (!ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            isIndexFingerPinching = false;
        }




        if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Middle) && !isMiddleFingerPinching)
        {
            isMiddleFingerPinching = true;
            DestroyAnchors(); 
        }

        if (!ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        {
            isMiddleFingerPinching = false;
        }
    }

    private void PlaceAnchor()
    {
        if(spatialAnchorCount < 4){
        AnchorSpawner.Instance.PlaceAnchorAtTransform(findFingerTip());

        }else{
        Debug.Log("AnchorInteraction: Anchor limit reached"); 
        }
    }

    public void LoadAnchors()
    {
        AnchorSession.Instance.QueryAllLocalAnchors();
    }

    public void DestroyAnchors()
    {

        Debug.Log("Destroying Anchors"); 
        foreach (ulong key in AnchorSession.Instance.handleToAnchor.Keys)
        {
            AnchorSession.Instance.EraseAnchor(key);
            break; 
        }

  
        
    }


    private Transform findFingerTip()
    {
        Transform interactionFingerTip;

        try
        {
            interactionFingerTip = ovrHand.PointerPose;

            interactionFingerTip.rotation = Quaternion.identity; 
            return interactionFingerTip;
        }
        catch
        {
            Debug.Log("AnchorInteraction: Fingertip not found");
            return ovrHand.transform;
        }
    }


}
