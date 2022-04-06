using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colTest : MonoBehaviour
{ void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ITWORJKS"); 
        gameObject.GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color; 
    }
}
