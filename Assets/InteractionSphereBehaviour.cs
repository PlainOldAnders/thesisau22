using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSphereBehaviour : MonoBehaviour
{
    public float range = 0.05f; 
    int frameSkip = 5; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        frameSkip++; 
        GameObject hand = GameObject.FindGameObjectsWithTag("Player")[0];
        
        OVRHand ovrhand = hand.GetComponent<OVRHand>();
        
        GameObject pointerPosition =  GameObject.FindGameObjectsWithTag("PointerFinger")[0];

        GameObject rovery = GameObject.FindGameObjectsWithTag("Rover")[0];

        if (hand.GetComponent<HandGrabbingBehaviour>().isPinching && Vector3.Distance(pointerPosition.transform.position, this.gameObject.transform.position) < range && frameSkip > 5)
        {
            Debug.Log("IM IN SPHERE PINVHING");
            rovery.GetComponent<RoverController>().roverDigMine();
            frameSkip = 0; 
        }
    }



}
