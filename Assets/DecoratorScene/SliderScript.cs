using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SliderScript : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    float sliderVal = 0.01f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

   // this.gameObject.GetComponent<Renderer>().material.color = GetColor();  
    
    }

    public Color GetColor(){
        return new Color(redSlider.value, greenSlider.value, blueSlider.value);  
    }
}
