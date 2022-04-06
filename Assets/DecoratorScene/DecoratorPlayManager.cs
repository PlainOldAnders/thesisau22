using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

using UnityEngine.Networking;

public class DecoratorPlayManager : MonoBehaviour
{

    public GameObject interactionSphere;

    public GameObject furnitureTarget; 

    public GameObject UIController;
    public string logIP = "192.168.53.34";
    public int funitureTotalNumber = 5;

    private int furniturePlacedNumber = 0;

    public int roundTotalNumber = 5;

    private int roundNumber = 1;

    public bool isGameFinished = false;

    public TextMeshProUGUI scoreText;

    private float timeSinceStart;

    public TextMeshProUGUI headerText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject UIinteractionTable = GameObject.FindGameObjectsWithTag("UIInteractionTable")[0];
        GameObject interactionTable = GameObject.FindGameObjectsWithTag("InteractionTable")[0];
        UIinteractionTable.transform.position = new Vector3 (interactionTable.transform.position.x, interactionTable.transform.position.y+0.01f,interactionTable.transform.position.z ); 
        UIinteractionTable.transform.parent = interactionTable.transform; 

        interactionTable.AddComponent<DecoratorInteractionTable>();
        Destroy(interactionTable.GetComponent<InteractionTableScript>());

        UIController.GetComponent<UIController>().setProgressButton(true);
        UIController.GetComponent<UIController>().setEntireUI(false);


        headerText.text = "Welcome!";
        scoreText.text = "Place all furniture around the room";
        StartCoroutine(getRequest(logIP, "-----------------------------------------"));
        StartCoroutine(getRequest(logIP, "Scene: Decorator - Number: " + funitureTotalNumber));
    }

    // Update is called once per frame
    void Update()
    {
   if (Input.GetKeyDown("space"))
        {
           //setFunitureTarget(); 
        }
    }



    public void placeNewFurniture()
    {
        GameObject interactionTable = GameObject.FindGameObjectsWithTag("InteractionTable")[0];

        interactionTable.SetActive(true);

        UIController.GetComponent<UIController>().setEntireUI(true);
        for (int j = 0; j < UIController.transform.childCount; j++)
        {
            Transform child = UIController.transform.GetChild(j);


            child.gameObject.SetActive(true);
            if (child.gameObject.tag == "SpawnerBut")
            {
                child.gameObject.SetActive(false);
            }

            /* if(child.childCount > 0){
                 for (int i = 0; i < child.GetChild(i).transform.childCount; i++){
                    child.GetChild(i).gameObject.SetActive(true); 
                    
                 }
             }*/
        }
        furniturePlacedNumber++;
        scoreText.text = furniturePlacedNumber.ToString() + " of " + funitureTotalNumber + "(" + roundNumber + ")";

        if (furniturePlacedNumber == funitureTotalNumber)
        {

            isGameFinished = true;
            StartCoroutine(getRequest(logIP, "Completion Time: " + (Time.time - timeSinceStart)));
            StartCoroutine(getRequest(logIP, "-----------------------------------------"));
            roundNumber++;
        }

        if (roundNumber > roundTotalNumber)
        {
            SceneManager.LoadScene("UserInterfaceScenario");
        }

    }

    public GameObject setFunitureTarget(){
        GameObject interactionTable = GameObject.FindGameObjectsWithTag("InteractionTable")[0];
        GameObject furnitureTargetOld = GameObject.FindGameObjectsWithTag("furnitureTarget")[0];
        Destroy(furnitureTargetOld); 
       
        var position = new Vector3(Random.Range(-2.5f, 2.5f), -0.2f, Random.Range(1, 2f));

         if(interactionTable != null ){
             position = new Vector3(interactionTable.transform.position.x + Random.Range(-2.5f, 2.5f), -0.2f,interactionTable.transform.position.z + Random.Range(1, 2f));
        } 
        GameObject furnitureTargetClone = Instantiate(furnitureTarget, position, Quaternion.identity); 
        
        return furnitureTargetClone; 

    }

    public void startGame()
    {
        isGameFinished = false;
        timeSinceStart = Time.time;
        furniturePlacedNumber = 0;

        scoreText.text = furniturePlacedNumber.ToString() + " of " + funitureTotalNumber;

    }



    public void placeInteractionSphere()
    {
        GameObject interactionTable = GameObject.FindGameObjectsWithTag("InteractionTable")[0];

        GameObject interactionSphereObject = Instantiate(interactionSphere, new Vector3(interactionTable.transform.position.x, interactionTable.transform.position.y, interactionTable.transform.position.z), Quaternion.identity);

        interactionSphereObject.transform.parent = interactionTable.transform;

        interactionSphereObject.AddComponent<DecoratorInteractionSphereBehaviour>();
        Destroy(interactionSphereObject.GetComponent<InteractionSphereBehaviour>());
    }



    IEnumerator getRequest(string uri, string text)
    {

        Debug.Log("Sending " + uri + "/" + text);
        UnityWebRequest uwr = UnityWebRequest.Get("http://" + uri + "/" + text);
        yield return uwr.SendWebRequest();
    }

}
