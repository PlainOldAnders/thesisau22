using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public GameObject minePrefab;
    private Vector3 newPos;
    private Vector3 startPosition;
    public bool redColorEnabled = true; 

    public bool hasMine = false;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        newPos = gameObject.transform.position;
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, Time.deltaTime * 5f);
    }


    private void OnTriggerEnter(Collider other)
    {
        newPos = new Vector3(startPosition.x, startPosition.y + 0.015f, startPosition.z);
        gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 3f);

    }

    void OnTriggerExit(Collider other)
    {
        newPos = startPosition;
        gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.5f);
    }

    public void setMine(bool state)
    {
        if (state)
        {
            hasMine = true;
            if(redColorEnabled){
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

        }else{
            hasMine = false; 
              gameObject.GetComponent<Renderer>().material.color = Color.white;

        }
    }

    

    public void digMine()
    {   
        ParticleSystem ps = GameObject.Find("Particle System").GetComponent<ParticleSystem>(); 
        ps.transform.position = transform.position; 
        ps.Play();
        GameObject g = Instantiate(minePrefab, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), transform.rotation);
        gameObject.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        g.GetComponent<Rigidbody>().AddForce(Vector3.up * 20f);
        setMine(false); 

        





    }
}
