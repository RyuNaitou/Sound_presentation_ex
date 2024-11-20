using UnityEngine;

public class TestSphere : MonoBehaviour
{
    [Tooltip("生成する音源オブジェクト")] public GameObject soundObjectPrefab;

    float maxAngleInterval = 30f;

    float radius = 5f;

    public Vector3 GetPointOnCylinder(float yaw, float pitch)
    {
        // 度をラジアンに変換
        float yawRad = Mathf.Deg2Rad * yaw;
        float pitchRad = Mathf.Deg2Rad * pitch;

        // 円柱座標からデカルト座標に変換
        float x = radius * Mathf.Sin(yawRad);
        float y = radius * Mathf.Sin(pitchRad);
        float z = -radius * Mathf.Cos(yawRad);

        return new Vector3(x, y, z);
    }
    public Vector3 GetPointOnSphere(float yaw, float pitch)
    {
        // 度をラジアンに変換
        float yawRad = Mathf.Deg2Rad * yaw;
        float pitchRad = Mathf.Deg2Rad * pitch;

        // 球面座標からデカルト座標に変換
        float x = radius * Mathf.Cos(pitchRad) * Mathf.Sin(yawRad);
        float y = radius * Mathf.Sin(pitchRad);
        float z = -radius * Mathf.Cos(pitchRad) * Mathf.Cos(yawRad);

        return new Vector3(x, y, z);
    }

    // テスト用
    void Start()
    {
        generateSoundObjects1LineEqually(5, -45);
        generateSoundObjects1LineEqually(5, 0);
        generateSoundObjects1LineEqually(5, 45);
    }

    void generateSoundObjects1LineEqually(int soundCount, float pitchAngle)
    {
        // 1行分の音源を等間隔に並べる

        // 最大角度間隔で並べても、180°以内に収まるか
        float angleInterval = (maxAngleInterval * soundCount <= 180f) ? maxAngleInterval : 180f / (soundCount - 1);

        // 左端のオブジェクトの角度を計算
        float startAngle = 180f - (angleInterval * (soundCount - 1)) / 2f;

        for (int i = 0; i < soundCount; i++)
        {
            float angle = startAngle + angleInterval * i;

            Vector3 point = GetPointOnCylinder(angle, pitchAngle);
            //Vector3 point = GetPointOnSphere(angle, pitchAngle);
            Instantiate(soundObjectPrefab, point, Quaternion.identity);
        }
    }

}
