using System;
using UnityEngine;

public static class PresentInfo
{
    // 現在の提示手法・提示個数・行数をメモ
    public static EXNAME exName;
    public static int soundNumber;
    public static int soundLineNumber;
    public static bool[,,] exFinished = new bool[6, 7, 3]; // 変更注意(提示間隔, 音源数, 行数)

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

        //// test
        //PresentInfo.exFinished[5,1,2] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEx(string exName)
    {
        PresentInfo.exName = (EXNAME)Enum.Parse(typeof(EXNAME), exName);
    }

    public void setNumber(int soundNumber)
    {
        PresentInfo.soundNumber = soundNumber;
    }

    public void setLineNumber(int soundLineNumber)
    {
        PresentInfo.soundLineNumber = soundLineNumber;
    }

    public void finishPreEx()
    {
        int exIndex = (int)PresentInfo.exName - 1;
        int soundNumber = PresentInfo.soundNumber - 4;
        int soundLineNumber = PresentInfo.soundLineNumber - 1;
        PresentInfo.exFinished[exIndex, soundNumber, soundLineNumber] = true;
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
