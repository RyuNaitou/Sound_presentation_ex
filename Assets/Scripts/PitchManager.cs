using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class PitchInfo
{
    [Tooltip("複数行での音源の縦の間隔角度")] public static int pitch = 30;
}

public class PitchManager : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Slider>().value = PitchInfo.pitch;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeSlider()
    {
        int value = (int)this.GetComponent<Slider>().value;

        valueText.text = $"{value}°";

        PitchInfo.pitch = value;
    }
}
