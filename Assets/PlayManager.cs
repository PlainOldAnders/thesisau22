using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

using UnityEngine.Networking;

public class PlayManager : MonoBehaviour
{

    public string logIP = "192.168.53.34";
    public int mineTotalNumber = 5;

    private int mineFoundNumber = 0;

    public int roundTotalNumber = 5; 

    private int roundNumber = 1; 

    public bool isGameFinished = false;

    public GameObject groundPlaceHolders;

    public TextMeshProUGUI scoreText;

    private float timeSinceStart; 

    public TextMeshProUGUI headerText;
    // Start is called before the first frame update
    void Start()
    {
        headerText.text = "Welcome!";
        scoreText.text = "Find and dig up all mines";
    StartCoroutine(getRequest(logIP, "-----------------------------------------"));
    StartCoroutine(getRequest(logIP, "Scene: Mines - Number: "+mineTotalNumber));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementMineFoundNumber()
    {
        
        mineFoundNumber++;
        scoreText.text = mineFoundNumber.ToString() + " of " + mineTotalNumber + "(" + roundNumber+ ")";

        if (mineFoundNumber == mineTotalNumber)
        {
            
            isGameFinished = true;
             StartCoroutine(getRequest(logIP, "Completion Time: " + (Time.time - timeSinceStart))); 
             StartCoroutine(getRequest(logIP, "-----------------------------------------"));
             roundNumber++; 
        }

        if(roundNumber > roundTotalNumber){
             SceneManager.LoadScene("UserInterfaceScenario");
        }

    }


    public void startGame()
    {
       isGameFinished = false; 
        timeSinceStart = Time.time; 
        mineFoundNumber = 0;
        eraseMines();
        scoreText.text = mineFoundNumber.ToString() + " of " + mineTotalNumber;
        placeMines(mineTotalNumber);
    }

    private void placeMines(int numberOfMines)
    {
        Component[] yourScripts = groundPlaceHolders.GetComponentsInChildren<GroundController>();
        //Create random values, and insert into list; 
        List<int> randomValues = new List<int>();
        for (int i = 0; i < numberOfMines; i++)
        {

            int randomValue = Random.Range(0, yourScripts.Length);

            if (!randomValues.Contains(randomValue))
            {
                randomValues.Add(randomValue);
            }
            else
            {
                numberOfMines = numberOfMines + 1;
            }
        }

        int counter = 0;
        foreach (GroundController script in yourScripts)
        {
            script.setMine(false);
            if (randomValues.Contains(counter))
            {
                script.setMine(true);
            }
            counter++;
        }

    }

    public void eraseMines()
    {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Mine");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }

        objects = GameObject.FindGameObjectsWithTag("PlaceHolders");
        Debug.Log("Objects " + objects.Length);
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<BoxCollider>().size = new Vector3(1f, 10f, 1f);
        }

    }

    IEnumerator getRequest(string uri, string text)
    {

        Debug.Log("Sending " + uri +"/"+ text); 
        UnityWebRequest uwr = UnityWebRequest.Get("http://" + uri +"/"+ text);
        yield return uwr.SendWebRequest();
    }

}
