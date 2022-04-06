using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ButtonScript : MonoBehaviour
{
    public GameObject interactionTable;

    public GameObject interactionSphere; 

    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("space"))
        {
           SceneManager.LoadScene("MineScenario");
        }

        if (AnchorSession.Instance.handleToAnchor.Count == 4)
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
            List<float> xPositions = new List<float>();
            List<float> zPositions = new List<float>();
            List<Anchor> anchors = new List<Anchor>();
            foreach (KeyValuePair<ulong, Anchor> kvp in AnchorSession.Instance.handleToAnchor)
            {
                //Save values in list
                anchors.Add(kvp.Value);

                //Save Anchors
                Debug.Log("SAVING ANCHORS" + kvp.Key.ToString());
                AnchorSession.Instance.SaveAnchor(kvp.Key, AnchorSession.StorageLocation.LOCAL);
            }

            var totalX = 0f;
            var totalY = 0f;
            var totalZ = 0f;

            //find average position of anchor and x and z points
            foreach (Anchor a in anchors)
            {
                xPositions.Add(a.transform.position.x);
                zPositions.Add(a.transform.position.z);

                totalX += a.transform.position.x;
                totalY += a.transform.position.y;
                totalZ += a.transform.position.z;
            }
            var centerX = totalX / anchors.Count;
            var centerY = totalY / anchors.Count;
            var centerZ = totalZ / anchors.Count;


            //Sort x and z points to find lowest and highest
            xPositions.Sort();
            zPositions.Sort();

            float xScale = (xPositions[xPositions.Count - 1] - xPositions[0]);
            float zScale = (zPositions[zPositions.Count - 1] - zPositions[0]);

            GameObject interactionBox = Instantiate(interactionTable, new Vector3(centerX, centerY, centerZ), Quaternion.identity);
            interactionBox.gameObject.transform.localScale = new Vector3(xScale, 0.05f, zScale);
            interactionBox.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);

            GameObject interactionTrigger = Instantiate(interactionSphere, new Vector3(centerX, centerY, centerZ), Quaternion.identity);
          
            interactionTrigger.transform.parent = interactionBox.transform; 
            
            SceneManager.LoadScene("MineScenario");
        }
    }

    void placeAnchorAndGoToScene(){


            List<float> xPositions = new List<float>();
            List<float> zPositions = new List<float>();
            List<Anchor> anchors = new List<Anchor>();
            foreach (KeyValuePair<ulong, Anchor> kvp in AnchorSession.Instance.handleToAnchor)
            {
                //Save values in list
                anchors.Add(kvp.Value);

                //Save Anchors
                Debug.Log("SAVING ANCHORS" + kvp.Key.ToString());
                AnchorSession.Instance.SaveAnchor(kvp.Key, AnchorSession.StorageLocation.LOCAL);
            }

            var totalX = 0f;
            var totalY = 0f;
            var totalZ = 0f;

            //find average position of anchor and x and z points
            foreach (Anchor a in anchors)
            {
                xPositions.Add(a.transform.position.x);
                zPositions.Add(a.transform.position.z);

                totalX += a.transform.position.x;
                totalY += a.transform.position.y;
                totalZ += a.transform.position.z;
            }
            var centerX = totalX / anchors.Count;
            var centerY = totalY / anchors.Count;
            var centerZ = totalZ / anchors.Count;


            //Sort x and z points to find lowest and highest
            xPositions.Sort();
            zPositions.Sort();

            float xScale = (xPositions[xPositions.Count - 1] - xPositions[0]);
            float zScale = (zPositions[zPositions.Count - 1] - zPositions[0]);

            GameObject interactionBox = Instantiate(interactionTable, new Vector3(centerX, centerY, centerZ), Quaternion.identity);
            interactionBox.gameObject.transform.localScale = new Vector3(xScale, 0.05f, zScale);
            interactionBox.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);

            GameObject interactionTrigger = Instantiate(interactionSphere, new Vector3(centerX, centerY, centerZ), Quaternion.identity);
          
            interactionTrigger.transform.parent = interactionBox.transform; 
            
            SceneManager.LoadScene("MineScenario");
        
    }
}
