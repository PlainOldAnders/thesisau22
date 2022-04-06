using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabbingBehaviour : OVRGrabber
{

    public bool isPinching = false;
    private OVRHand m_hand;
    public float pinchTreshold = 0.7f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_hand = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        checkIndexPinch();
    }

    void checkIndexPinch()
    {
      
        float pinchStrength = m_hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (!m_grabbedObj && pinchStrength > pinchTreshold )//&& m_grabCandidates.Count > 0)
        {
            Debug.Log("PINCH"); 
            isPinching = true; 
            //GrabBegin();
        }
        //Remember to check grabbed object
        else if (pinchStrength < pinchTreshold)
        {
            isPinching = false; 
            //GrabEnd();
            //GetComponent<ObjectController>().OnGrabEnd();
        }
    }
}


