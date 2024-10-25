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
    public List<SoundData> usingSoundDatas;

    public GameObject soundObjectPrefab;
    public List<GameObject> soundObjects;

    public float radius;

    public int correctSoundIndex;
    SoundData correctSound;
    bool isPresentSounds;

    // Start is called before the first frame update
    void Start()
    {
        initPresentationScene();
        initPresentationStep();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    void generateSoundObjects(int exNumber)
    {
        soundObjects = new List<GameObject>();
        for(int i = 0; i < exNumber; i++)
        {
            float angle = (180 / (exNumber - 1)) * i;
            float x, z;
            x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            // 音源インスタンス生成
            soundObjects.Add(Instantiate(soundObjectPrefab, new Vector3(x, 0, z), Quaternion.identity));
        }
    }

    public void initPresentationStep()
    {
        // 音源の配置をランダム化
        usingSoundDatas = usingSoundDatas.OrderBy(a => Guid.NewGuid()).ToList();

        // 対象音源を設定
        correctSoundIndex = UnityEngine.Random.Range(0, usingSoundDatas.Count);
        correctSound = usingSoundDatas[correctSoundIndex];
    }

}
