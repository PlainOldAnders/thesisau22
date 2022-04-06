using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTableScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hand;

    void Awake()
    {
           DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COllisiton");

        if (other.tag == "Player")
        {

        }
    }




    private void OnTriggerStay(Collider other)
    {   

        GameObject pointerPosition =  GameObject.FindGameObjectsWithTag("PointerFinger")[0];

        if(other.gameObject.tag == "PointerFinger"){
        GameObject rovery = GameObject.FindGameObjectsWithTag("Rover")[0];
        float speed = 0.05f;

        float distance = Vector3.Distance(pointerPosition.transform.position, transform.position);

        speed = (distance + 1) * speed;

        var heading = pointerPosition.transform.position - transform.position;

         if(rovery.transform.localPosition.x < -0.121 && heading.x < 0){
            heading.x = 0; 
        }

        if(rovery.transform.localPosition.x >1.5 && heading.x > 0){
            heading.x = 0; 
        }

        if(rovery.transform.localPosition.z < -0.78 && heading.z < 0){
            heading.z= 0; 
        }

        if(rovery.transform.localPosition.z >0.735 && heading.z > 0){
            heading.z = 0; 
        }



        Vector3 moveDir = new Vector3(heading.x, 0.0f, heading.z);

        rovery.transform.position += moveDir * speed;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("COllisiton");

        if (other.tag == "Player")
        {

        }
    }






}
