using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpatialTestManager : MonoBehaviour
{
    [Header("アタッチ")]
    [Tooltip("生成する音源オブジェクト")] public GameObject soundObjectPrefab;
    [Tooltip("生成した音源のリンク")] List<GameObject> soundObjects;
    [Tooltip("使う音源")] public AudioClip testSoundClip;

    [Header("ハイパーパラメータ")]
    [Tooltip("最大角度間隔")] public float maxAngleInterval;
    [Tooltip("音源までの距離")] public float radius;

    [Tooltip("音源数")] public int testSoundCount;


    // Start is called before the first frame update
    void Start()
    {
        generateSoundObjects();

        for (int i = 0; i < testSoundCount; i++)
        {
            soundObjects[i].GetComponent<AudioSource>().clip = testSoundClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateSoundObjects()
    {
        soundObjects = new List<GameObject>();
        generateSoundObjects1LineEqually(testSoundCount, 0);
    }

    void generateSoundObjects1LineEqually(int soundCount, float pitchAngle)
    {
        // 1行分の音源を等間隔に並べる(円柱状)

        // 最大角度間隔で並べても、180°以内に収まるか
        float angleInterval = (maxAngleInterval * soundCount <= 180f) ? maxAngleInterval : 180f / (soundCount - 1);

        // 左端のオブジェクトの角度を計算
        float startAngle = 180f - (angleInterval * (soundCount - 1)) / 2f;

        for (int i = 0; i < soundCount; i++)
        {
            float angle = startAngle + angleInterval * i;

            float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // 左からなのでマイナス
            float y = Mathf.Sin(Mathf.Deg2Rad * pitchAngle) * radius;
            float z = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            // 音源インスタンス生成
            soundObjects.Add(Instantiate(soundObjectPrefab, new Vector3(x, y, z), Quaternion.identity));

        }
    }

}
