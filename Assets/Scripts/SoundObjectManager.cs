using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectManager : MonoBehaviour
{
    [Tooltip("この音源の高さ")] float pitchAngel;
    [Tooltip("この音源の水平方向の位置")] float yawAngle;

    [Tooltip("水平方向の減少量の重み")] float weight = 1f;
    [Tooltip("アタッチされた音源")] AudioSource audioSource;

    [Tooltip("リスナーの向き")] public Transform listenerTransform;

    // Start is called before the first frame update
    void Start()
    {
        listenerTransform = GameObject.Find("/Listener").transform;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float volume = 1f;
        if(ExParameter.isHorizonalChangeVolume && PresentInfo.soundLineNumber == 1)
        {
            volume = volume * changeVolumefromYaw();
        }
        if(ExParameter.isVerticalChangeVolume && PresentInfo.soundLineNumber != 1)
        {
            volume = volume * changeVolumefromPitch();
        }

        audioSource.volume = volume;
    }

    float changeVolumefromPitch()
    {
        // リスナーの向いている高さによってボリュームを変更

        float volume = 1f;

        if (pitchAngel != null)
        {
            // リスナーのピッチを取得

            // オブジェクトのローカル回転をQuaternionで取得(Unityの使用上90°で何故か反転するため、Quaternionから計算)
            Quaternion rotation = listenerTransform.localRotation;

            // ピッチ角度を計算（前方ベクトルから算出）
            float listenerPitch = Mathf.Atan2(2f * (rotation.w * rotation.x + rotation.y * rotation.z),
                                      1f - 2f * (rotation.x * rotation.x + rotation.y * rotation.y)) * Mathf.Rad2Deg;

            //0~360のため、-180~180に
            if (listenerPitch > 180)
            {
                listenerPitch = listenerPitch - 360f;
            }
            listenerPitch = -listenerPitch; // 左回りが正のため

            //Debug.Log($"Pitch:{listenerPitch}");


            // 高さの差から音量の大きさを変更
            //volume = 1f - Mathf.Abs(listenerPitch - pitchAngel) / 180f;
            // なだらかに
            float normalized4 = Mathf.Pow((listenerPitch - pitchAngel) / 180f, 4);
            volume = 1f - normalized4 / (0.003f + normalized4);

        }

        return volume;
    }
    float changeVolumefromYaw()
    {
        // リスナーの向いている水平方向の向きによってボリュームを変更

        float volume = 1f;

        if (yawAngle != null)
        {
            // リスナーの水平方向の向きを取得
            float listenerYaw = listenerTransform.eulerAngles.y;
            //0~360のため、-180~180に
            if(listenerYaw > 180)
            {
                listenerYaw = listenerYaw - 360f;
            }
            //Debug.Log($"Yaw:{listenerYaw}");

            // 向きの差から音量の大きさを変更
            volume = 1f - (Mathf.Abs(listenerYaw - yawAngle) / 120f) * weight;
        }

        return volume;
    }

    public void setAngles(float yawAngleValue, float pitchAngleValue)
    {
        yawAngle = yawAngleValue;
        pitchAngel = pitchAngleValue;
    }
}
