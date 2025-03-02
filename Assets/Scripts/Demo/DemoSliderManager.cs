using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class DemoParameter
{
    [Tooltip("デモモード")] public static bool isDemo = false;
}

public class DemoSliderManager : MonoBehaviour
{
    public enum ParameterType
    {
        DEMO,
    }

    public ParameterType parameterType;

    public TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        switch (parameterType)
        {
            case ParameterType.DEMO:
                this.GetComponent<Slider>().value = Convert.ToInt32(ExParameter.isCheat);
                break;
        }

        changeSlider();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeSlider()
    {
        int value = (int)this.GetComponent<Slider>().value;

        switch (parameterType)
        {
            case ParameterType.DEMO:
                if (Convert.ToBoolean(value))
                {
                    valueText.text = "ON";
                }
                else
                {
                    valueText.text = "OFF";
                }

                DemoParameter.isDemo = Convert.ToBoolean(value);

                break;
        }

    }
}
