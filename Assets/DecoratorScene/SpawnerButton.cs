using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButton : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject interactionTable;
    private GameObject clone;

    public GameObject UIScreen;

    public GameObject UITable;

    // Start is called before the first frame update
    void Start()
    {
        interactionTable = GameObject.FindGameObjectsWithTag("InteractionTable")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            UIScreen.GetComponent<UIController>().setEntireUI(false);
            UITable.GetComponent<UIController>().setEntireUI(false);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            UIScreen.GetComponent<UIController>().spawnFurniture(0);
            //UIScreen.GetComponent<UIController>().setEntireUI(false);
          //  UITable.GetComponent<UIController>().setEntireUI(false);
           // this.gameObject.SetActive(false);
        }



    }



    private void OnTriggerEnter(Collider other)
    {
       
        
            UIScreen.GetComponent<UIController>().spawnFurniture(0);
            UIScreen.GetComponent<UIController>().setEntireUI(false);
            UITable.GetComponent<UIController>().setEntireUI(false);
            this.gameObject.SetActive(false);
        
    }
}