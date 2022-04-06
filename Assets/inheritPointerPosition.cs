using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inheritPointerPosition : MonoBehaviour
{
    public GameObject hand; 
    private OVRHand ovrHand; 
    // Start is called before the first frame update
    void Start()
    {
        ovrHand = hand.GetComponent<OVRHand>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = ovrHand.GetComponent<OVRHand>().PointerPose.transform.position; 
    }
}
