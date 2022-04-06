    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MineButtonScript : MonoBehaviour
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

         if (Input.GetKeyDown(KeyCode.X))
        {
            isGameStarted = true; 
             playManager.GetComponent<PlayManager>().startGame();
        }

        
        if ( !isGameStarted || playManager.GetComponent<PlayManager>().isGameFinished)
        {
            button.SetActive(true);

        }
        else
        {
            button.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COllisiton");

        if (other.tag == "Player")
        {
            isGameStarted = true; 
            playManager.GetComponent<PlayManager>().startGame();

        }
    }
}

