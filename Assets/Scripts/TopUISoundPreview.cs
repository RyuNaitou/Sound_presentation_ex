using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUISoundPreview : MonoBehaviour
{
    public GameObject TopUISoundPreviewPrefab;

    public SoundManager soundManager;

    int radius = 100;

    float anchorPosY;

    // Start is called before the first frame update
    void Start()
    {
        anchorPosY = -radius / 2f;
        generateTopUISoundPreviews();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void generateTopUISoundPreviews()
    {
        // 上から見た音源配置プレビューを生成(一番音源が多い行)

        // 一番音源が多い行の音源数を取得
        int maxSoundCount = 0;
        switch (PresentInfo.soundLineNumber)
        {
            case 1:
                maxSoundCount = PresentInfo.soundNumber;
                break;
            case 2:
                maxSoundCount = Mathf.Max(SoundCountLine.Line2[PresentInfo.soundNumber - 4, 0], SoundCountLine.Line2[PresentInfo.soundNumber - 4, 1]);
                break;
            case 3:
                maxSoundCount = Mathf.Max(SoundCountLine.Line3[PresentInfo.soundNumber - 4, 0], SoundCountLine.Line3[PresentInfo.soundNumber - 4, 1], SoundCountLine.Line3[PresentInfo.soundNumber - 4, 2]);
                break;
        }

        // 最大角度間隔で並べても、180°以内に収まるか
        float angleInterval = (soundManager.maxAngleInterval * maxSoundCount <= 180f) ? soundManager.maxAngleInterval : 180f / (maxSoundCount - 1);

        // 左端のプレビューの角度を計算
        float startAngle = 180f - (angleInterval * (maxSoundCount - 1)) / 2f;

        for (int i = 0; i < maxSoundCount; i++)
        {
            float angle = startAngle + angleInterval * i;

            float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // 左からなのでマイナス
            float y = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius + anchorPosY;

            // プレビューインスタンス生成
            GameObject topUISoundPreview =  Instantiate(TopUISoundPreviewPrefab, new Vector3(x, y, 0), Quaternion.identity);
            topUISoundPreview.transform.SetParent(transform, false);
        }
    }
}
