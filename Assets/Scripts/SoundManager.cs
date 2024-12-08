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
    public Sprite sprite;
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

    [Tooltip("実験情報制御スクリプト")] public PresentationInfoManager presentationInfoManager;
    [Tooltip("対象音源表示スクリプト")] public TargetSoundInfoManager targetSoundInfoManager;

    [Tooltip("全音源")] public List<SoundData> AllSoundDatas;
    [Tooltip("使う音源")] List<SoundData> usingSoundDatas;

    [Tooltip("生成する音源オブジェクト")] public GameObject soundObjectPrefab;
    [Tooltip("生成した音源のリンク")] List<GameObject> soundObjects;

    [Tooltip("対象音源確認用オブジェクト")] public AudioSource targetSoundTestAudioSource;

    [Tooltip("使用不可パネル")] public GameObject targetButtonDisablePanel;
    [Tooltip("使用不可パネル")] public GameObject startButtonDisablePanel;
    [Tooltip("使用不可パネル")] public GameObject selectButtonDisablePanel;


    [Header("ハイパーパラメータ")]
    [Tooltip("音源までの距離")] public float radius;
    [Tooltip("最大角度間隔")] public float maxAngleInterval;
    // [Tooltip("複数行でのピッチ角")] public float pitchAngleInterval;  ExParameterManagerで制御するため削除

    [Tooltip("対象音源の場所")] int targetSoundIndex;
    [Tooltip("対象音源情報")] SoundData targetSound;
    [Tooltip("音源提示中かどうか")] bool isPresentEx;

    [Tooltip("音源提示順序のための攪乱順列")] Queue<int> randomSequenceQueue;

    [Tooltip("提示間隔")] float presentInterval;
    //[Tooltip("次に全音源開始するまでの時間")] public float presentAllInterval = 7f;     ExParameterManagerで制御するため削除
    //[Tooltip("(不必要？)次に提示する音源の場所")] int nextPresentSoundIndex = 0;

    [Tooltip("前回音源提示した時間")] float lastPresentTime = -100;
    [Tooltip("前回全音源を提示し始めた時間")] float lastAllPresentTime = -100;
    [Tooltip("全音源を提示し始めて良いか")] bool isPresentAll;

    [Tooltip("実験開始時間")] float startExTime = float.NaN;

    //[Tooltip("タスク回数")] public int maxTaskCount;   ExParameterManagerで制御するため削除

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
            // 実験開始時間をメモ
            if (float.IsNaN(startExTime))
            {
                startExTime = Time.time;
            }

            // 一連の音源提示の間隔(音源提示はじめから[?]秒)
            float now = Time.time;
            if(now - lastAllPresentTime > ExParameter.presentAllInterval)
            {
                isPresentAll = true;
                lastAllPresentTime = now;
            }

            // 各音源の提示
            if(isPresentAll)
            {
                presentNextSound();
                
                // 一連の音源を提示し終わったら、次の間隔まで中断
                if(randomSequenceQueue.Count <= 0)
                {
                    initRandomSequenceQueue();
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
                generateSoundObjects1LineEqually(SoundCountLine.Line2[exNumber - 4, 0], ExParameter.pitch);
                generateSoundObjects1LineEqually(SoundCountLine.Line2[exNumber - 4, 1], -ExParameter.pitch);

                break;
            case 3:
                generateSoundObjects1LineEqually(SoundCountLine.Line3[exNumber - 4, 0], ExParameter.pitch);
                generateSoundObjects1LineEqually(SoundCountLine.Line3[exNumber - 4, 1], 0);
                generateSoundObjects1LineEqually(SoundCountLine.Line3[exNumber - 4, 2], -ExParameter.pitch);

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

            // 音源オブジェクトにピッチの情報を代入
            soundObjects.Last().GetComponent<SoundObjectManager>().setAngles(angle-180f, pitchAngle);
        }
    }

    public void initPresentationStep()
    {
        // 実験開始時間を初期化
        startExTime = float.NaN;

        // 音源の配置をランダム化
        usingSoundDatas = usingSoundDatas.OrderBy(a => Guid.NewGuid()).ToList();

        // 音源オブジェクトに音源ファイルをアタッチ
        for(int i = 0;i < usingSoundDatas.Count; i++)
        {
            soundObjects[i].GetComponent<AudioSource>().clip = usingSoundDatas[i].clip;
            //soundObjects[i].GetComponent<AudioSource>().Play();
        }

        // 音源の提示順序のための攪乱順列を初期化
        initRandomSequenceQueue();

        // 対象音源を設定
        targetSoundIndex = UnityEngine.Random.Range(0, usingSoundDatas.Count);
        targetSound = usingSoundDatas[targetSoundIndex];
        targetSoundTestAudioSource.clip = soundObjects[targetSoundIndex].GetComponent<AudioSource>().clip;

        // 対象音源情報を更新
        targetSoundInfoManager.changeTargetInfo(targetSound);

        // スタート・選択ボタンを押せないように(対象音源を聞いてから音源提示・選択する)
        startButtonDisablePanel.SetActive(true);
        selectButtonDisablePanel.SetActive(true);
    }

    void initRandomSequenceQueue()
    {
        // 音源の提示順序のための攪乱順列(乱列)を生成

        // 音源の数分の数字(順列)をリストに追加
        List<int> permutation = Enumerable.Range(0, usingSoundDatas.Count).ToList();

        // ランダムオブジェクトを作成
        System.Random random = new System.Random();

        // リストをランダムにシャッフル
        for (int i = permutation.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            // 数字を交換
            (permutation[i], permutation[j]) = (permutation[j], permutation[i]);
        }

        // シャッフルされたリストからキューを作成
        randomSequenceQueue = new Queue<int>(permutation);

    }

    public void presentNextSound()
    {
        // 提示間隔を見て各音源を提示
        float now = Time.time;
        if(now - lastPresentTime > presentInterval)
        {
            soundObjects[randomSequenceQueue.Dequeue()].GetComponent<AudioSource>().Play();
            lastPresentTime = now;
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
        
        // 実験時間
        float responseTime = Time.time - startExTime;

        // 情報をメモ(選択音源のインデックスを保存)
        presentationInfoManager.initExStepMemo(targetSound.type, targetSoundIndex, buttonIndex, responseTime);
    }
}
