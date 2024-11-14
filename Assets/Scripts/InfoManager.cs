using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Info
{
    //public enum EXFREQUENCY
    //{
    //    ALLWAYS,
    //    EXPERIENCED,
    //    NEVER
    //}
    public static string date;
    public static string name;
    //public static int age;
    //public static EXFREQUENCY exFrequency;
}

public class InfoManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField ageInputField;
    public TMP_Dropdown exFrequencyDropDown;

    public void inputName()
    {
        Info.name = nameInputField.text;
    }
    //public void inputAge()
    //{
    //    Info.age = int.Parse(ageInputField.text);
    //}
    //public void inputExFrequency()
    //{
    //    Info.exFrequency = (Info.EXFREQUENCY)exFrequencyDropDown.value;
    //}
}
