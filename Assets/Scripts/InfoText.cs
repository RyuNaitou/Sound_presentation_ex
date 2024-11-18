using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    // 下部の表記用
    public enum InfoType
    {
        name,
        present,
        number
    }

    public InfoType infoType;

    Dictionary<EXNAME, string> presentInfoTextDic = new Dictionary<EXNAME, string>()
    {
        //{EXNAME.Same, "同時提示" },
        {EXNAME.Continuous_01, "連続提示(0.1秒)" },
        {EXNAME.Continuous_02, "連続提示(0.2秒)" },
        {EXNAME.Continuous_03, "連続提示(0.3秒)" },
        {EXNAME.Continuous_04, "連続提示(0.4秒)" },
        {EXNAME.Continuous_05, "連続提示(0.5秒)" },
        {EXNAME.Continuous_06, "連続提示(0.6秒)" }
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string infoText = "";
        switch (infoType)
        {
            case InfoType.name:
                infoText = Info.name;
                break;
            case InfoType.present:
                infoText = presentInfoTextDic[PresentInfo.exName];
                break;
            case InfoType.number:
                infoText = "音源" + PresentInfo.soundNumber.ToString() + "個(" + PresentInfo.soundLineNumber + "行)";
                break;
        }

        this.GetComponent<TextMeshProUGUI>().text = infoText;
    }
}
