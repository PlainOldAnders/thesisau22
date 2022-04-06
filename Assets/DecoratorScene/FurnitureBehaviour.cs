using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FurnitureBehaviour : MonoBehaviour
{
    public bool hasBeenPlaced = false;
    private GameObject playManager;
    public bool shouldRotate = true;
    private Transform indicatorPos; 

    public GameObject furnitureIndicator;

    private GameObject furnitureTarget;
    // Start is called before the first frame update
    void Start()
    {
        playManager = GameObject.FindGameObjectsWithTag("PlayManager")[0];
        furnitureTarget = playManager.GetComponent<DecoratorPlayManager>().setFunitureTarget();
        GameObject furnitureIndicatorClone =  Instantiate(furnitureIndicator, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.2f, gameObject.transform.position.z), Quaternion.identity);

        furnitureIndicatorClone.transform.parent = this.gameObject.transform;
 //object1 is now the child of object2
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRotate)
        {
            this.gameObject.transform.Rotate(0, 0.5f, 0 * Time.deltaTime); //rotates 50 degrees per second around z axis
        }
    }


    public void fallDown()
    {
        if (!hasBeenPlaced)
        {
            StartCoroutine(MoveToSpot(this.gameObject));
        }
        hasBeenPlaced = true;
    }

    public void animateForward()
    {
        StartCoroutine(animateToFront(this.gameObject));
    }


    IEnumerator animateToFront(GameObject furniture)
    {
        Vector3 Gotoposition = new Vector3(furniture.transform.position.x, furniture.transform.position.y, furniture.transform.position.z + 0.4f);
        float elapsedTime = 0;
        float waitTime = 3f;
        Vector3 currentPos = furniture.transform.position;

        while (elapsedTime < waitTime)
        {
            furniture.transform.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        furniture.transform.position = Gotoposition;



        elapsedTime = 0;
        waitTime = 2f;
        int scaleVal = 3;
        Vector3 newScale = new Vector3(furniture.transform.localScale.x * scaleVal, furniture.transform.localScale.y * scaleVal, furniture.transform.localScale.z * scaleVal);


        while (elapsedTime < waitTime)
        {
            furniture.transform.localScale = Vector3.Lerp(furniture.transform.localScale, newScale, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;

        shouldRotate = false;
    }

    IEnumerator MoveToSpot(GameObject furniture)
    {
        Vector3 Gotoposition = new Vector3(furniture.transform.position.x, furniture.transform.position.y - 0.3f, furniture.transform.position.z);
        float elapsedTime = 0;
        float waitTime = 1f;
        Vector3 currentPos = furniture.transform.position;

        indicatorPos = furnitureIndicator.transform; 

        while (elapsedTime < waitTime)
        {
            furniture.transform.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            furnitureIndicator.transform.position = indicatorPos.position; 
            yield return null;
        }
        furniture.transform.position = Gotoposition;

        GameObject.FindGameObjectsWithTag("UIInteractionTable")[0].GetComponent<UIController>().setProgressButton(true);
    }
}
