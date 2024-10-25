using System;
using UnityEngine;

public static class PresentInfo
{
    // 現在の提示手法・提示個数をメモ
    public static EXNAME exName;
    public static int soundNumber;
    public static bool[,] exFinished = new bool[7, 9];

    // 実験の状況をメモ
    public static int exCount;
    public static string tempCSVText;
}

public class PresentationInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        initExMemo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEx(string exName)
    {
        PresentInfo.exName = (EXNAME)Enum.Parse(typeof(EXNAME), exName);
    }

    public void setNum(int soundNumber)
    {
        PresentInfo.soundNumber = soundNumber;
    }

    public void finishPreEx()
    {
        int exIndex = (int)PresentInfo.exName;
        int soundNumber = PresentInfo.soundNumber;
        PresentInfo.exFinished[exIndex, soundNumber] = true;
    }

    public void initExMemo()
    {
        PresentInfo.exCount = 0;
        PresentInfo.tempCSVText = "対象音,正解の場所,回答した場所,正誤,回答時間\n";
    }

    public void writeCSVText(string targetSound, int correctSoundIndex, int answerSoundIndex, float responseTime)
    {
        int correction = Convert.ToInt32(correctSoundIndex == answerSoundIndex);

        PresentInfo.tempCSVText += (targetSound + ",");
        PresentInfo.tempCSVText += (correctSoundIndex.ToString() + ",");
        PresentInfo.tempCSVText += (answerSoundIndex.ToString() + ",");
        PresentInfo.tempCSVText += (correction.ToString() + ",");
        PresentInfo.tempCSVText += (responseTime.ToString() + "\n");
    }
}
