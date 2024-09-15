using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wwise_test : MonoBehaviour
{
    public AK.Wwise.RTPC wwise_test_slider;
    public float slider_float;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().value = 50.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        slider_float = gameObject.GetComponent<Slider>().value;
        wwise_test_slider.SetGlobalValue(slider_float);
        // wwise_test_slider.SetGlobalValue(x);

    }
}
