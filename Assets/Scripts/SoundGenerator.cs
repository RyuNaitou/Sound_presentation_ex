using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public enum SOUNDTYPE
{
    SOUND0,
    SOUND1,
    SOUND2,
    SOUND3,
    SOUND4,
    SOUND5,
    SOUND6,
    SOUND7,
    SOUND8,
    SOUND9,
}

[System.Serializable]
public class SoundData
{
    public SOUNDTYPE type;
    public AudioClip clip;
}

public class SoundGenerator : MonoBehaviour
{

    public List<SoundData> AllSoundDatas;
    List<SoundData> usingSoundDatas;

    public GameObject soundObjectPrefab;
    List<GameObject> soundObjects;

    public AudioSource targetSoundTestAudioSource;

    public GameObject targetButtonDisablePanel;
    public GameObject startButtonDisablePanel;
    public GameObject selectButtonDisablePanel;

    public float radius;

    int correctSoundIndex;
    SoundData correctSound;
    bool isPresentEx;

    public float presentInterval;
    public float presentAllInterval = 7f;
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
                presentSounds();
                
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
            case EXNAME.Same:
                presentInterval = 0f; break;
            case EXNAME.Com1:
                presentInterval = 0.1f; break;
            case EXNAME.Com2:
                presentInterval = 0.2f; break;
            case EXNAME.Com3:
                presentInterval = 0.3f; break;
            case EXNAME.Com4:
                presentInterval = 0.4f; break;
            case EXNAME.Com5:
                presentInterval = 0.5f; break;
            case EXNAME.Com6:
                presentInterval = 0.6f; break;
        }
    }

    void generateSoundObjects(int exNumber)
    {
        // 音源の個数から自動的に配置を設定し、音源オブジェクトを生成
        soundObjects = new List<GameObject>();
        for(int i = 0; i < exNumber; i++)
        {
            float angle = (180 / (exNumber - 1)) * i;
            float x, z;
            x = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius; // 左からなのでマイナス
            z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            // 音源インスタンス生成
            soundObjects.Add(Instantiate(soundObjectPrefab, new Vector3(x, 0, z), Quaternion.identity));
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

    public void presentSounds()
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
        // 対象音源が再生されている間は、音源提示と選択ボタンを使えないように
        while (targetSoundTestAudioSource.isPlaying)
        {
            yield return null;
        }

        startButtonDisablePanel.SetActive(false);
        selectButtonDisablePanel.SetActive(false);
    }
}
