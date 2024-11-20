using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public enum SOUNDTYPE
{
    XYLOPHONE,
    BELL,
    CAT,
    PIANO,
    BUBBLE,
    TRUMPET,
    DOG,
    FLUTE,
    SEAGULL,
    CORRECT,
}

[System.Serializable]
public class SoundData
{
    public SOUNDTYPE type;
    public AudioClip clip;
}

public static class SoundCountLine
{
    // 音源の配置データ(読み取りのみ)

    // 各行数の音源の個数
    public static readonly int[,] Line2 = new int[7, 2]
    {
        { 2, 2 },
        { 3, 2 },
        { 3, 3 },
        { 4, 3 },
        { 4, 4 },
        { 5, 4 },
        { 5, 5 },
    };
    public static readonly int[,] Line3 = new int[7, 3] 
    {
        { 1, 2, 1 },
        { 2, 1, 2 },
        { 2, 2, 2 },
        { 2, 3, 2 },
        { 3, 2, 3 },
        { 3, 3, 3 },
        { 3, 4, 3 },
    };
}

public class SoundManager : MonoBehaviour
{

    [Header("アタッチ")]

    [Tooltip("全音源")] public List<SoundData> AllSoundDatas;
    List<SoundData> usingSoundDatas;

    [Tooltip("生成する音源オブジェクト")] public GameObject soundObjectPrefab;
    List<GameObject> soundObjects;

    [Tooltip("対象音源確認用オブジェクト")]
    public AudioSource targetSoundTestAudioSource;

    [Tooltip("使用不可パネル")] public GameObject targetButtonDisablePanel;
    [Tooltip("使用不可パネル")] public GameObject startButtonDisablePanel;
    [Tooltip("使用不可パネル")] public GameObject selectButtonDisablePanel;


    [Header("音源配置設定")]
    [Tooltip("音源までの距離")] public float radius;
    [Tooltip("最大角度間隔")] public float maxAngleInterval;
    [Tooltip("複数行でのピッチ角")] public float pitchAngleInterval;

    int correctSoundIndex;
    SoundData correctSound;
    bool isPresentEx;

    float presentInterval;
    [Tooltip("次に音源開始するまでの時間")] public float presentAllInterval = 7f;
    int nextPresentSoundIndex = 0;

    float lastPresentTime = -100;
    float lastAllPresentTime = -100;
    bool isPresentAll;

    // Start is called before the first frame update
    void Start()
    {
        initPresentationScene();
        initPresentationStep();
    }

    // Update is called once per frame
    void Update()
    {
        // "音源提示スタート"ボタンが押されたら提示開始
        if (isPresentEx)
        {
            // 一連の音源提示の間隔
            float now = Time.time;
            if(now - lastAllPresentTime > presentAllInterval)
            {
                isPresentAll = true;
                lastAllPresentTime = now;
            }

            // 各音源の提示
            if(isPresentAll)
            {
                presentNextSound();
                
                // 一連の音源を提示し終わったら、次の間隔まで中断
                if(nextPresentSoundIndex >= usingSoundDatas.Count)
                {
                    nextPresentSoundIndex = 0;
                    isPresentAll = false;
                }
            }
        }
    }

    public void initPresentationScene()
    {
        // 使用音源だけ抽出
        usingSoundDatas = new List<SoundData>();
        int exNumber = PresentInfo.soundNumber;
        for(int i = 0; i < exNumber; i++)
        {
            usingSoundDatas.Add(AllSoundDatas[i]);
        }

        // 音源オブジェクトを配置
        generateSoundObjects(exNumber);

        // 提示間隔の設定
        switch (PresentInfo.exName)
        {
            //case EXNAME.Same:
            //    presentInterval = 0f; break;
            case EXNAME.Continuous_01:
                presentInterval = 0.1f; break;
            case EXNAME.Continuous_02:
                presentInterval = 0.2f; break;
            case EXNAME.Continuous_03:
                presentInterval = 0.3f; break;
            case EXNAME.Continuous_04:
                presentInterval = 0.4f; break;
            case EXNAME.Continuous_05:
                presentInterval = 0.5f; break;
            case EXNAME.Continuous_06:
                presentInterval = 0.6f; break;
        }
    }

    void generateSoundObjects(int exNumber)
    {
        soundObjects = new List<GameObject>();
        switch (PresentInfo.soundLineNumber)
        {
            case 1:
                generateSoundObjects1LineEqually(exNumber, 0);

                break;
            case 2:
                generateSoundObjects1LineEqually(SoundCountLine.Line2[exNumber - 4, 0], -pitchAngleInterval);
                generateSoundObjects1LineEqually(SoundCountLine.Line2[exNumber - 4, 1], pitchAngleInterval);

                break;
            case 3:
                generateSoundObjects1LineEqually(SoundCountLine.Line3[exNumber - 4, 0], -pitchAngleInterval);
                generateSoundObjects1LineEqually(SoundCountLine.Line3[exNumber - 4, 1], 0);
                generateSoundObjects1LineEqually(SoundCountLine.Line3[exNumber - 4, 2], pitchAngleInterval);

                break;
        }
    }

    void generateSoundObjects1LineEqually(int soundCount, float pitchAngle)
    {
        // 1行分の音源を等間隔に並べる(円柱状)

        // 最大角度間隔で並べても、180°以内に収まるか
        float angleInterval = (maxAngleInterval * soundCount <= 180f) ? maxAngleInterval : 180f / (soundCount-1);

        // 左端のオブジェクトの角度を計算
        float startAngle = 180f - (angleInterval * (soundCount - 1)) / 2f;

        for(int i = 0;i < soundCount;i++)
        {
            float angle = startAngle + angleInterval * i;

            float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // 左からなのでマイナス
            float y = Mathf.Sin(Mathf.Deg2Rad * pitchAngle) * radius;
            float z = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            // 音源インスタンス生成
            soundObjects.Add(Instantiate(soundObjectPrefab, new Vector3(x, y, z), Quaternion.identity));

        }
    }

    public void initPresentationStep()
    {
        // 音源の配置をランダム化
        usingSoundDatas = usingSoundDatas.OrderBy(a => Guid.NewGuid()).ToList();

        // 音源オブジェクトに音源ファイルをアタッチ
        for(int i = 0;i < usingSoundDatas.Count; i++)
        {
            soundObjects[i].GetComponent<AudioSource>().clip = usingSoundDatas[i].clip;
            //soundObjects[i].GetComponent<AudioSource>().Play();
        }

        // 対象音源を設定
        correctSoundIndex = UnityEngine.Random.Range(0, usingSoundDatas.Count);
        correctSound = usingSoundDatas[correctSoundIndex];
        targetSoundTestAudioSource.clip = soundObjects[correctSoundIndex].GetComponent<AudioSource>().clip;
    }

    public void presentNextSound()
    {
        // 提示間隔を見て各音源を提示
        float now = Time.time;
        if(now - lastPresentTime > presentInterval)
        {
            soundObjects[nextPresentSoundIndex].GetComponent<AudioSource>().Play();
            lastPresentTime = now;
            nextPresentSoundIndex++;
        }
    }

    public void changeIsPresentEx(bool status)
    {
        // UIから音源提示フラグを設定
        isPresentEx = status;
    }

    public void listenTarget()
    {
        // 音源提示中なら止める
        if (isPresentEx)
        {
            stopAllSounds();
            isPresentEx = false;
            lastAllPresentTime = -100;
        }

        // 音源提示と選択ボタンを使えないように
        startButtonDisablePanel.SetActive(true);
        selectButtonDisablePanel.SetActive(true);

        targetSoundTestAudioSource.Play();

        // 対象音源の提示が終わったら、音源提示と選択ボタンを使えるように
        StartCoroutine(waitForTarget());
    }

    void stopAllSounds()
    {
        // 提示音源をすべて止める
        for(int i = 0; i < soundObjects.Count; i++)
        {
            soundObjects[i].GetComponent<AudioSource>().Stop();
        }
    }

    IEnumerator waitForTarget()
    {
        // 対象音源が再生されている間は、音源提示ボタンと選択ボタンを使えないように
        while (targetSoundTestAudioSource.isPlaying)
        {
            yield return null;
        }

        startButtonDisablePanel.SetActive(false);
        selectButtonDisablePanel.SetActive(false);
    }

    public void selectButtonAction(int buttonIndex)
    {
        // 選択ボタンが押されたときの処理

        // 音源提示を終了
        changeIsPresentEx(false);

        // 選択音源のインデックスを保存

    }
}
