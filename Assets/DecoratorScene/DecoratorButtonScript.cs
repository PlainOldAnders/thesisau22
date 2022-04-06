  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecoratorButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playManager;

    public GameObject button;

    public TextMeshProUGUI buttonText;

    private bool isGameStarted = false; 



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetKeyDown(KeyCode.A))
        {
           playManager.GetComponent<DecoratorPlayManager>().placeNewFurniture(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COllisiton");

        if (other.tag == "Player")
        {
            Debug.Log("Pressed");
            if(GameObject.FindGameObjectsWithTag("InteractionSphere").Length != 0){
            Destroy(GameObject.FindGameObjectWithTag("InteractionSphere"));
            }
            playManager.GetComponent<DecoratorPlayManager>().placeNewFurniture(); 

        }
    }
}

