using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class ExParameter
{
    [Tooltip("チートモード")] public static bool isCheat = false;
    [Tooltip("水平方向のソニフィケーション(1行のみ)をするか")] public static bool isHorizonalChangeVolume = true;
    [Tooltip("垂直方向のソニフィケーションをするか")] public static bool isVerticalChangeVolume = true;
    [Tooltip("複数行での音源の縦の間隔角度")] public static int pitch = 45;
    [Tooltip("実験回数")] public static int maxTaskCount = 10;
    //[Tooltip("すべての音源を提示する間隔(最初の提示から?秒)")] public static int presentAllInterval = 0;
    [Tooltip("すべての音源を提示する間隔(最後の提示から?秒)")] public static int presentEndInterval = 2;
}

public class ExParameterSliderManager : MonoBehaviour
{
    public enum ParameterType
    {
        Cheat,
        PITCH,
        MAX_TASK_COUNT,
        PRESENT_ALL_INTERVAL,
        PRESENT_END_INTERVAL,
    }

    public ParameterType parameterType;

    public TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        switch (parameterType)
        {
            case ParameterType.Cheat:
                this.GetComponent<Slider>().value = Convert.ToInt32(ExParameter.isCheat);
                break;
            case ParameterType.PITCH:
                this.GetComponent<Slider>().value = ExParameter.pitch;
                break;
            case ParameterType.MAX_TASK_COUNT:
                this.GetComponent<Slider>().value = ExParameter.maxTaskCount;
                break;
            //case ParameterType.PRESENT_ALL_INTERVAL:
            //    this.GetComponent<Slider>().value = ExParameter.presentAllInterval;
            //    break;
            case ParameterType.PRESENT_END_INTERVAL:
                this.GetComponent<Slider>().value = ExParameter.presentEndInterval;
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
            case ParameterType.Cheat:
                if (Convert.ToBoolean(value))
                {
                    valueText.text = "ON";
                }
                else
                {
                    valueText.text = "OFF";
                }

                ExParameter.isCheat = Convert.ToBoolean(value);

                break;
            case ParameterType.PITCH:
                valueText.text = $"{value}°";

                ExParameter.pitch = value;
                
                break;
            case ParameterType.MAX_TASK_COUNT:
                valueText.text = $"{value}回";

                ExParameter.maxTaskCount = value;

                break;
            //case ParameterType.PRESENT_ALL_INTERVAL:
            //    valueText.text = $"{value}秒";

            //    ExParameter.presentAllInterval = value;

            //    break;
            case ParameterType.PRESENT_END_INTERVAL:
                valueText.text = $"{value}秒";

                ExParameter.presentEndInterval = value;

                break;
        }

    }
}
