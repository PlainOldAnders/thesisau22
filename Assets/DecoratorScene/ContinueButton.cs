using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ContinueButton : MonoBehaviour
{
    public GameObject spawnerButton; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Tab))
        {
           print("DUKCING INter"); 
        Debug.Log("asdawda"); 
        GameObject[] sliders = GameObject.FindGameObjectsWithTag("UISlider");

        foreach (GameObject s in sliders){
            s.SetActive(false); 
        }

        spawnerButton.SetActive(true); 
        this.gameObject.SetActive(false);
        }
    }

     private void OnTriggerEnter(Collider other)
    {

        print("DUKCING INter"); 
        Debug.Log("asdawda"); 
        GameObject[] sliders = GameObject.FindGameObjectsWithTag("UISlider");

        foreach (GameObject s in sliders){
            s.SetActive(false); 
        }

        spawnerButton.SetActive(true); 
        this.gameObject.SetActive(false); 
    }
}
