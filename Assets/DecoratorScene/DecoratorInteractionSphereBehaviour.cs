using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorInteractionSphereBehaviour : MonoBehaviour
{
    public float range = 0.05f; 
    int frameSkip = 5; 
    public float pinchTime = 2; 

    float elapsedPinchingTime; 
    bool isStartedPinchingTime; 
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
       List<GameObject> furnitures =new List<GameObject>(GameObject.FindGameObjectsWithTag("Furniture")); 
        GameObject furniture = furnitures[furnitures.Count-1];
     

        if (hand.GetComponent<HandGrabbingBehaviour>().isPinching && Vector3.Distance(pointerPosition.transform.position, this.gameObject.transform.position) < range && frameSkip > 5)
        {
            if(!isStartedPinchingTime){
            elapsedPinchingTime = Time.time; 
            isStartedPinchingTime = true; 
            }
            
            if(Time.time - elapsedPinchingTime > pinchTime){
            Debug.Log("IM IN SPHERE PINVHING");
            furniture.GetComponent<FurnitureBehaviour>().fallDown(); 
            frameSkip = 0; 
            elapsedPinchingTime = 0; 
            isStartedPinchingTime  =false; 
            }
        }
    }



}
