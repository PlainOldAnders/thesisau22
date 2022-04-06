using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private Vector3 initialPos;
    private Quaternion initialRot;

    public float rangeThreshold = 0.5f;

    public GameObject placeHolderParent;


    void Start()
    {
        initialPos = transform.position;
        initialRot = transform.rotation;

        SphereCollider myCollider = transform.GetComponent<SphereCollider>();
        myCollider.radius = rangeThreshold*2; // or whatever radius you want.
    }

    [ContextMenu("OnGrabEnd")]
    public void OnGrabEnd()
    {

      placeObject(); 
    }

    

    void placeObject()
    {
        List<GameObject> placeHolderList = getPlaceHolders(placeHolderParent);

        float closestChildDistance = 1000f;
        GameObject closestChildObject = null;

        for (int i = 0; i < placeHolderList.Count; i++)
        {

            float distanceToObject = Vector3.Distance(placeHolderList[i].transform.position, transform.position);
            if (distanceToObject < rangeThreshold)
            {

                if (distanceToObject < closestChildDistance){
                    closestChildDistance = distanceToObject;
                    closestChildObject = placeHolderList[i];
                }

            }

        }

        if(closestChildObject == null){
            transform.position = initialPos; 
            transform.rotation = initialRot; 
            Debug.Log("Could not find place"); 
        }else{
            transform.position = new Vector3(closestChildObject.transform.position.x,closestChildObject.transform.position.y + 0.02f, closestChildObject.transform.position.z ); 
        
             transform.rotation = closestChildObject.transform.rotation; 
            Debug.Log("Object Placed"); 
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlaceHolders"){
        Debug.Log("Entered " + other.name);
        other.GetComponent<Renderer> ().material.color = Color.red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlaceHolders"){
        other.GetComponent<Renderer> ().material.color = Color.gray;
         }
    }


    List<GameObject> getPlaceHolders(GameObject placeHolders)
    {

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < placeHolders.transform.childCount; i++)
        {
            list.Add(placeHolders.transform.GetChild(i).gameObject);
        }
        return list;
    }
}