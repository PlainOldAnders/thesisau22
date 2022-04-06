using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorInteractionTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerStay(Collider other)
    {

        GameObject pointerPosition = GameObject.FindGameObjectsWithTag("PointerFinger")[0];

        if (other.gameObject.tag == "PointerFinger")
        {
            List<GameObject> roveries = new List<GameObject>(GameObject.FindGameObjectsWithTag("Furniture"));
            GameObject rovery = roveries[roveries.Count - 1];

            if (!rovery.GetComponent<FurnitureBehaviour>().hasBeenPlaced)
            {
                float speed = 0.05f;

                float distance = Vector3.Distance(pointerPosition.transform.position, transform.position);

                speed = (distance + 1) * speed;

                var heading = pointerPosition.transform.position - transform.position;

                Vector3 moveDir = new Vector3(heading.x, 0.0f, heading.z);

                rovery.transform.position += moveDir * speed;
            }
        }
    }
}
