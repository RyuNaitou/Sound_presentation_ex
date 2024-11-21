using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresentaionInfoDropdownManager : MonoBehaviour
{
    // ドロップダウンUIからPresentationInfoの変数のセッターを呼び出す。

    public PresentationInfoManager PresentationInfoManagerScript;

    public enum DROPDOWNTYPE
    {
        NUMBER,
        LINENUMBER,
    }

    public DROPDOWNTYPE dropdownType;

    // Start is called before the first frame update
    void Start()
    {
        switch (dropdownType)
        {
            case DROPDOWNTYPE.NUMBER:
                this.GetComponent<TMP_Dropdown>().value = PresentInfo.soundNumber - 4;
                
                break;
            case DROPDOWNTYPE.LINENUMBER:
                this.GetComponent<TMP_Dropdown>().value = PresentInfo.soundLineNumber - 1;

                break;
        }
    }

    public void setSoundNumberFromDropdown()
    {
        // 音源数のドロップダウンの場合(4-10個)
        PresentationInfoManagerScript.setNumber(this.GetComponent<TMP_Dropdown>().value + 4);
    }
    public void setSoundLineNumberFromDropdown()
    {
        // 音源数のドロップダウンの場合(1-3行)
        PresentationInfoManagerScript.setLineNumber(this.GetComponent<TMP_Dropdown>().value + 1);
    }
}
