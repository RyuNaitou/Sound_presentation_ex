using System;
using UnityEngine;

public static class PresentInfo
{
    // 現在の提示手法・提示個数・行数をメモ
    public static EXNAME exName = EXNAME.Continuous_06;
    public static int soundNumber = 4;
    public static int soundLineNumber = 2;
    public static bool[,,] exFinished = new bool[6, 7, 3]; // 変更注意(提示間隔, 音源数, 行数)

    // 実験の状況をメモ
    public static int exCount;
    public static string CSVTextBuffer;
}

public class PresentationInfoManager : MonoBehaviour
{
    public CSVManager csvManager;

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
        // CSVに書き込み
        csvManager.createCSV(PresentInfo.exName, $"{PresentInfo.soundNumber}-{PresentInfo.soundLineNumber}", PresentInfo.CSVTextBuffer);

        // 完了マークの処理
        int exIndex = (int)PresentInfo.exName - 1;
        int soundNumber = PresentInfo.soundNumber - 4;
        int soundLineNumber = PresentInfo.soundLineNumber - 1;
        PresentInfo.exFinished[exIndex, soundNumber, soundLineNumber] = true;
    }

    public void initExMemo()
    {
        // 実験情報の初期化

        PresentInfo.exCount = 0;
        PresentInfo.CSVTextBuffer = "対象音,正解の場所,回答した場所,正誤,回答時間\n";
    }

    public void initExStepMemo(SOUNDTYPE targetSoundType,int targetSoundIndex, int answerSoundIndex, float responseTime)
    {
        // 実験のステップごとの処理

        // CSV保存バッファにメモ
        writeCSVTextBuffer(targetSoundType, targetSoundIndex, answerSoundIndex, responseTime);
        
        // 実験回数をメモ
        PresentInfo.exCount++;
    }

    public void writeCSVTextBuffer(SOUNDTYPE targetSoundType, int targetSoundIndex, int answerSoundIndex, float responseTime)
    {
        // CSV保存バッファにメモ

        // 正誤判定(false:0, true:1)
        int isValid = Convert.ToInt32(targetSoundIndex == answerSoundIndex);

        // バッファに保存
        PresentInfo.CSVTextBuffer += $"{targetSoundType.ToString()},{targetSoundIndex},{answerSoundIndex},{isValid},{responseTime}\n";
    }
}
