using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISoundPreview : MonoBehaviour
{
    public GameObject UISoundPreviewPrefab;

    List<GameObject> UISoundPreviews = new List<GameObject>();

    int maxPosXInterval = 90;
    int posYInterval = 70;

    int buttonSize = 30;

    // Start is called before the first frame update
    void Start()
    {
        generateUISoundPreviews();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeUISoundsPreview()
    {
        clearUISoundPreviews();
        generateUISoundPreviews();
    }

    void clearUISoundPreviews()
    {
        for (int i = 0; i < UISoundPreviews.Count; i++)
        {
            Destroy(UISoundPreviews[i]);
        }

        UISoundPreviews.Clear();
    }

    void generateUISoundPreviews()
    {
        // 音源配置に合わせてプレビューを生成
        int allSoundCount = PresentInfo.soundNumber;
        switch (PresentInfo.soundLineNumber)
        {
            case 1:
                generateUISoundPreviews1LineEqually(allSoundCount, 0);

                break;
            case 2:
                generateUISoundPreviews1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 0], posYInterval);
                generateUISoundPreviews1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 1], -posYInterval);

                break;
            case 3:
                generateUISoundPreviews1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 0], posYInterval);
                generateUISoundPreviews1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 1], 0);
                generateUISoundPreviews1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 2], -posYInterval);

                break;
        }
    }

    void generateUISoundPreviews1LineEqually(int soundCount, int posY)
    {
        // 1行分のプレビューを等間隔に並べる

        // 最大間隔で並べても、パネル内に収まるか
        float panelWidth = this.GetComponent<RectTransform>().sizeDelta.x - 50 - buttonSize;    // 50:パディング
        float posXInterval = (maxPosXInterval * soundCount <= panelWidth) ? maxPosXInterval : panelWidth / (soundCount - 1);

        // 左端のプレビューのX座標を計算
        float startPosX = -(posXInterval / 2f) * (soundCount - 1);

        for (int i = 0; i < soundCount; i++)
        {
            float posX = startPosX + posXInterval * i;

            float x = posX;
            float y = posY;

            // プレビューインスタンス生成
            GameObject uiSoundPreview = Instantiate(UISoundPreviewPrefab, new Vector3(x, y, 0), Quaternion.identity);
            uiSoundPreview.transform.SetParent(transform, false);

            UISoundPreviews.Add(uiSoundPreview);
        }
    }

}
