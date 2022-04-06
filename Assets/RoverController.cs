using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    public float speed = 0.01f;

    public GameObject playManager; 
    public GameObject communicationManager; 

    // Start is called before the first frame update
    private int frameSkip = 0; 
    private GameObject hoverObject; 
    private bool isHoveringOverMine = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        sendSearchFeedBack(); 
        if (Input.GetKeyDown("space"))
        {
            if(isHoveringOverMine){
                 communicationManager.GetComponent<comScript>().sendHardAttract("225", "CCW", "500");
                hoverObject.GetComponent<GroundController>().digMine(); 
                playManager.GetComponent<PlayManager>().incrementMineFoundNumber(); 
            }
        }


        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");


        if(transform.localPosition.x < -0.121 && xDir < 0){
            xDir = 0; 
        }

        if(transform.localPosition.x >1.5 && xDir > 0){
            xDir = 0; 
        }

        if(transform.localPosition.z < -0.78 && zDir < 0){
            zDir = 0; 
        }

        if(transform.localPosition.z >0.735 && zDir > 0){
            zDir = 0; 
        }



        Vector3 moveDir = new Vector3(xDir, 0.0f, zDir);

        transform.position += moveDir * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        hoverObject = other.gameObject; 
        if (hoverObject.GetComponent<GroundController>().hasMine)
        {
            isHoveringOverMine = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        isHoveringOverMine = false;
    }

    public void roverDigMine(){

        if(isHoveringOverMine){
                communicationManager.GetComponent<comScript>().sendHardAttract("225", "CCW", "500");
                hoverObject.GetComponent<GroundController>().digMine(); 
                playManager.GetComponent<PlayManager>().incrementMineFoundNumber(); 
            }
    }

   private void sendSearchFeedBack(){
      float power =  getClosestMinePower(); 
      int powerInt = (int) power; 
        frameSkip++; 
        if(isHoveringOverMine && frameSkip > 120){
            communicationManager.GetComponent<comScript>().sendVibrate(powerInt.ToString(), "10", "20", "both"); 
            frameSkip = 0; 
        }else if (frameSkip> 120){
              communicationManager.GetComponent<comScript>().sendVibrate(powerInt.ToString(), "10", "20", "both"); 
            frameSkip = 0; 
        }
    
    }

    private float getClosestMinePower(){
        float power = 0f; 
        float closestDistance  = 1000f; 


        GameObject[] mines = GameObject.FindGameObjectsWithTag("PlaceHolders");


        foreach (GameObject mine in mines)
        {
           if(mine.GetComponent<GroundController>().hasMine){
               float distance =  Vector3.Distance(mine.transform.position, this.gameObject.transform.position); 
            if(distance < closestDistance){
                closestDistance = distance;  
            }
           }
        }
        
        if(closestDistance < 999){
        power = 255 - (closestDistance * 200);  
        }else{
            power = 0; 
        }

        if(power > 255){
            power = 255; 
        }

        if(power < 0 ){
            power = 0; 
        }

        print(power); 
        return power; 


    }
}
