using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ExMODE
{
    Ex = 0,
    HTEx = 1,
    Demo = 2,
}

public static class ModeParameter
{
    [Tooltip("モードタイプ")] public static ExMODE exMode = ExMODE.Ex;
}

public class ModeSliderManager : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Slider>().value = (float)ModeParameter.exMode;

        changeSlider();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeSlider()
    {
        int value = (int)this.GetComponent<Slider>().value;

        switch ((ExMODE)value)
        {
            case ExMODE.Ex:
                valueText.text = "音源特定";
                break;
            case ExMODE.HTEx:
                valueText.text = "HT実験";
                break;
            case ExMODE.Demo:
                valueText.text = "デモ";
                break;
        }
        ModeParameter.exMode = (ExMODE)value;

    }
}
