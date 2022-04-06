using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject PlayManager; 
    public List<GameObject> furniture;
    public GameObject colorObject;
    private GameObject interactionTable;
    private GameObject clone;

    public GameObject SpawnerBut; 

    public GameObject progressButton; 


    bool furnitureHasMoved; 


    // Start is called before the first frame update
    void Start()
    {
        SpawnerBut.SetActive(false); 
        interactionTable = GameObject.FindGameObjectsWithTag("InteractionTable")[0];
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    public void setEntireUI(bool state){
        Debug.Log("SET UI ACTIvE"); 
        this.gameObject.SetActive(state); 
    }

    public void setProgressButton(bool state){
        progressButton.SetActive(state); 
    }

    public void spawnFurniture(int number)
    {

        clone = Instantiate(furniture[number], new Vector3(interactionTable.transform.position.x, interactionTable.transform.position.y + 0.05f, interactionTable.transform.position.z), transform.rotation);
        clone.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        clone.GetComponent<Renderer>().material.color = colorObject.GetComponent<SliderScript>().GetColor();
        clone.GetComponent<FurnitureBehaviour>().animateForward(); 
        PlayManager.GetComponent<DecoratorPlayManager>().placeInteractionSphere(); 
        
    }



   

}
