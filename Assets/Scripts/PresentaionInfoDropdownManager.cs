using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresentaionInfoDropdownManager : MonoBehaviour
{
    // �h���b�v�_�E��UI����PresentationInfo�̕ϐ��̃Z�b�^�[���Ăяo���B

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
        // �������̃h���b�v�_�E���̏ꍇ(4-10��)
        PresentationInfoManagerScript.setNumber(this.GetComponent<TMP_Dropdown>().value + 4);
    }
    public void setSoundLineNumberFromDropdown()
    {
        // �������̃h���b�v�_�E���̏ꍇ(1-3�s)
        PresentationInfoManagerScript.setLineNumber(this.GetComponent<TMP_Dropdown>().value + 1);
    }
}
