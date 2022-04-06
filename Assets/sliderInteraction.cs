using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sliderInteraction : MonoBehaviour
{
    public GameObject SliderKnob;


    public Slider UISlider; 

    public GameObject sliderPercentage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        //SliderKnob.transform.localPosition = new Vector3(SliderKnob.transform.localPosition.x-10, SliderKnob.transform.localPosition.y, SliderKnob.transform.localPosition.z); 
         float percent = (Mathf.InverseLerp(-112, 69, SliderKnob.transform.localPosition.x) * 100);
        if (SliderKnob.transform.localPosition.x > 69)
        {
            SliderKnob.transform.localPosition = new Vector3(69, SliderKnob.transform.localPosition.y, SliderKnob.transform.localPosition.z);
        }
        else if (SliderKnob.transform.localPosition.x < -112)
        {
            SliderKnob.transform.localPosition = new Vector3(-112, SliderKnob.transform.localPosition.y, SliderKnob.transform.localPosition.z);
        }

        sliderPercentage.GetComponent<Text>().text = (int) percent + "%"; 
        UISlider.value = percent/100; 

    }
}
