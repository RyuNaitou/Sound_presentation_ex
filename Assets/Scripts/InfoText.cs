using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    public enum InfoType
    {
        name,
        present,
        number
    }

    public InfoType infoType;

    Dictionary<EXNAME, string> presentInfoTextDic = new Dictionary<EXNAME, string>()
    {
        {EXNAME.Same, "同時提示" },
        {EXNAME.Com1, "連続提示(0.1秒)" },
        {EXNAME.Com2, "連続提示(0.2秒)" },
        {EXNAME.Com3, "連続提示(0.3秒)" },
        {EXNAME.Com4, "連続提示(0.4秒)" },
        {EXNAME.Com5, "連続提示(0.5秒)" },
        {EXNAME.Com6, "連続提示(0.6秒)" }
    };

    // Start is called before the first frame update
    void Start()
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
                infoText = "音源" + PresentInfo.soundNumber.ToString() + "個";
                break;
        }

        this.GetComponent<TextMeshProUGUI>().text = infoText;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
