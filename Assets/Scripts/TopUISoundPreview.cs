using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUISoundPreview : MonoBehaviour
{
    public GameObject TopUISoundPreviewPrefab;

    public SoundManager soundManager;

    float horizonalPadding = 40f;
    
    float radius;

    float anchorPosY;

    float posYInterval = 10;

    float sizeInterval = 5;

    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<RectTransform>().sizeDelta.y - 2 * horizonalPadding;
        anchorPosY = -radius / 2f;
        generateLisnerPreview();
        generateTopUISoundPreviews();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void generateLisnerPreview()
    {
        // リスナーの場所のプレビューを生成

        // プレビューインスタンス生成
        GameObject listenerPreview = Instantiate(TopUISoundPreviewPrefab, new Vector3(0, anchorPosY, 0), Quaternion.identity);
        listenerPreview.GetComponent<Image>().color = Color.white;
        listenerPreview.transform.SetParent(transform, false);
    }

    //void generateTopUISoundPreviewsMaxLine()
    //{
    //    // 上から見た音源配置プレビューを生成(一番音源が多い行)

    //    // 一番音源が多い行の音源数を取得
    //    int maxSoundCount = 0;
    //    switch (PresentInfo.soundLineNumber)
    //    {
    //        case 1:
    //            maxSoundCount = PresentInfo.soundNumber;
    //            break;
    //        case 2:
    //            maxSoundCount = Mathf.Max(SoundCountLine.Line2[PresentInfo.soundNumber - 4, 0], SoundCountLine.Line2[PresentInfo.soundNumber - 4, 1]);
    //            break;
    //        case 3:
    //            maxSoundCount = Mathf.Max(SoundCountLine.Line3[PresentInfo.soundNumber - 4, 0], SoundCountLine.Line3[PresentInfo.soundNumber - 4, 1], SoundCountLine.Line3[PresentInfo.soundNumber - 4, 2]);
    //            break;
    //    }

    //    // 最大角度間隔で並べても、180°以内に収まるか
    //    float angleInterval = (soundManager.maxAngleInterval * maxSoundCount <= 180f) ? soundManager.maxAngleInterval : 180f / (maxSoundCount - 1);

    //    // 左端のプレビューの角度を計算
    //    float startAngle = 180f - (angleInterval * (maxSoundCount - 1)) / 2f;

    //    for (int i = 0; i < maxSoundCount; i++)
    //    {
    //        float angle = startAngle + angleInterval * i;

    //        float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // 左からなのでマイナス
    //        float y = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius + anchorPosY;

    //        // プレビューインスタンス生成
    //        GameObject topUISoundPreview = Instantiate(TopUISoundPreviewPrefab, new Vector3(x, y, 0), Quaternion.identity);
    //        topUISoundPreview.transform.SetParent(transform, false);
    //    }
    //}

    void generateTopUISoundPreviews()
    {
        // 上から見た音源配置プレビューを生成

        // 音源配置に合わせてプレビューを生成
        int allSoundCount = PresentInfo.soundNumber;
        switch (PresentInfo.soundLineNumber)
        {
            case 1:
                generateTopUISoundPreviews1LineEqually(allSoundCount, 0, new Color(1, 0.5f, 0.2f), 0);

                break;
            case 2:
                // 重なりのため下から生成
                generateTopUISoundPreviews1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 1], -posYInterval, Color.red, -sizeInterval);
                generateTopUISoundPreviews1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 0], posYInterval, Color.yellow, sizeInterval);

                break;
            case 3:
                // 重なりのため下から生成
                generateTopUISoundPreviews1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 2], -posYInterval, Color.red, -sizeInterval);
                generateTopUISoundPreviews1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 1], 0, new Color(1, 0.5f, 0.2f), 0);
                generateTopUISoundPreviews1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 0], posYInterval, Color.yellow, sizeInterval);

                break;
        }
    }

    void generateTopUISoundPreviews1LineEqually(int soundCount, float offsetY, Color previewColor, float offsetSize)
    {
        // 上から見た音源配置プレビューを1行分生成

        // 最大角度間隔で並べても、180°以内に収まるか
        float angleInterval = (soundManager.maxAngleInterval * soundCount <= 180f) ? soundManager.maxAngleInterval : 180f / (soundCount - 1);

        // 左端のプレビューの角度を計算
        float startAngle = 180f - (angleInterval * (soundCount - 1)) / 2f;

        for (int i = 0; i < soundCount; i++)
        {
            float angle = startAngle + angleInterval * i;

            float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // 左からなのでマイナス
            float y = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius + anchorPosY + offsetY;

            // プレビューインスタンス生成
            GameObject topUISoundPreview = Instantiate(TopUISoundPreviewPrefab, new Vector3(x, y, 0), Quaternion.identity);
            topUISoundPreview.GetComponent<RectTransform>().sizeDelta += new Vector2(offsetSize, offsetSize);
            topUISoundPreview.GetComponent<Image>().color = previewColor;
            topUISoundPreview.transform.SetParent(transform, false);
        }
    }
}