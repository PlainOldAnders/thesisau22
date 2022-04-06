using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.colorfulcoding.customVRLogic
{
    public class ObjectGrabbable : OVRGrabbable
    {
        private GameObject chosenGameObject;

        protected override void Start()
        {
            base.Start();
            chosenGameObject = GetComponent<GameObject>();
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
           GetComponent<ObjectController>().OnGrabEnd(); 
        }

    }
}
