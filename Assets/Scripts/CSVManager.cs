using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public enum EXNAME
{
    //Same = 0,
    Continuous_01 = 1,
    Continuous_02 = 2,
    Continuous_03 = 3,
    Continuous_04 = 4,
    Continuous_05 = 5,
    Continuous_06 = 6,
    info = -1,
}

public static class CSVInfo
{
    public static string filePrefix = "";
    public static string foldername = "";
    //public 
}

public class CSVManager : MonoBehaviour
{
    void createFolder()
    {
        //iCloudバックアップ不要設定
        UnityEngine.iOS.Device.SetNoBackupFlag(Application.persistentDataPath);

        // フォルダパス
        string folderPath = Path.Combine(Application.persistentDataPath, CSVInfo.foldername);
#if UNITY_EDITOR
        // ファイルパス
        folderPath = CSVInfo.foldername;
#endif

        // フォルダが存在しない場合は作成
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);

            //　各実験のフォルダを作成
            //List<string> exNames = new List<string> { "Same", "Continuous_01", "Continuous_02", "Continuous_03", "Continuous_04", "Continuous_05", "Continuous_06" };
            List<string> exNames = new List<string> { "Continuous_01", "Continuous_02", "Continuous_03", "Continuous_04", "Continuous_05", "Continuous_06" };

            foreach (string exName in exNames)
            {
                Directory.CreateDirectory(Path.Combine(folderPath, exName));
            }
        }
    }

    public void createCSV(EXNAME exname, string infoname, string contents)
    {

        // ファイル名の設定(参加者情報、実験２など)
        string fileName = CSVInfo.filePrefix + infoname + ".csv";

        string filePath;
        // ファイルパス(実験フォルダに入れる(infoはそのまま))
        if (exname != EXNAME.info)
        {
            filePath = Path.Combine(Application.persistentDataPath, CSVInfo.foldername, exname.ToString(), fileName);
        }
        else
        {
            filePath = Path.Combine(Application.persistentDataPath, CSVInfo.foldername, fileName);
        }

#if UNITY_EDITOR
        // ファイルパス(実験フォルダに入れる(infoはそのまま))
        if (exname != EXNAME.info)
        {
            filePath = Path.Combine(CSVInfo.foldername, exname.ToString(), fileName);
        }
        else
        {
            filePath = Path.Combine(CSVInfo.foldername, fileName);
        }
#endif

        // ファイルが存在しなければ新規作成
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // CSVの内容
                sw.WriteLine(contents);
                //string participantData = "名前," + Info.name + "\n" + "年齢," + Info.age.ToString() + "\n" + "空間音響体験頻度," + Info.exFrequency;
            }
        }
    }

    public void createInfoCSV()
    {
        Info.date = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        //string contents = "日時," + Info.date + "\n" + "名前," + Info.name + "\n" + "年齢," + Info.age.ToString() + "\n" + "空間音響体験頻度," + Info.exFrequency;
        string contents = "日時," + Info.date + "\n" + "名前," + Info.name;

        // 参加者情報を記録
        if (CSVInfo.filePrefix == "")
        {
            CSVInfo.filePrefix = Info.name + "_";
        }
        // 参加者フォルダを指定
        if (CSVInfo.foldername == "")
        {
            CSVInfo.foldername = System.DateTime.Now.ToString("yyyyMMdd_HHmm_") + Info.name;
        }
        createFolder();


        createCSV(EXNAME.info, "info", contents);
    }
}
