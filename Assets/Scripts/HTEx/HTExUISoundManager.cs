using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HTExUISoundManager : MonoBehaviour
{
    public GameObject UISoundPrefab;

    public List<GameObject> uiSoundObjects;

    //public SoundManager soundManager;

    int maxPosXInterval = 180;
    int posYInterval = 150;

    //int anchorPosY = 80;

    int buttonSize = 80;

    int nextButtonIndex = 0;

    //public GameObject isPresentPanel;
    //public GameObject nextPanel;
    //public GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        // UIボタンの生成
        generateUISounds();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void generateUISounds()
    {
        // 音源配置に合わせてUI選択ボタンを生成
        int allSoundCount = PresentInfo.soundNumber;
        switch (PresentInfo.soundLineNumber)
        {
            case 1:
                generateUISound1LineEqually(allSoundCount, 0);

                break;
            case 2:
                //generateUISound1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 0], posYInterval);
                generateUISound1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 0], 0);  // 正面
                generateUISound1LineEqually(SoundCountLine.Line2[allSoundCount - 4, 1], -posYInterval);

                break;
            case 3:
                generateUISound1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 0], posYInterval);
                generateUISound1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 1], 0);
                generateUISound1LineEqually(SoundCountLine.Line3[allSoundCount - 4, 2], -posYInterval);

                break;
        }
    }

    void generateUISound1LineEqually(int soundCount, int posY)
    {
        // 1行分のボタンを等間隔に並べる

        // 最大間隔で並べても、パネル内に収まるか
        float panelWidth = this.GetComponent<RectTransform>().sizeDelta.x - 50 - buttonSize;    // 50:パディング
        float posXInterval = (maxPosXInterval * soundCount <= panelWidth) ? maxPosXInterval : panelWidth / (soundCount - 1);

        // 左端のボタンのX座標を計算
        float startPosX = -(posXInterval / 2f) * (soundCount - 1);

        for (int i = 0; i < soundCount; i++)
        {
            float posX = startPosX + posXInterval * i;

            float x = posX;
            float y = posY;
            //float y = posY + anchorPosY;

            // ボタンインスタンス生成
            GameObject uiSound = Instantiate(UISoundPrefab, new Vector3(x, y, 0), Quaternion.identity);
            uiSound.transform.SetParent(transform, false);
            uiSound.transform.SetSiblingIndex(2);    // DisablePanelよりは下に

            // ボタン番号の設定
            int buttonIndex = nextButtonIndex;  // クロージャ問題を回避するためローカル変数に置き換え
            uiSound.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonIndex.ToString();
            //uiSound.GetComponent<Button>().onClick.AddListener(() => UISoundAction(buttonIndex));

            //// チートモードでの画像表示
            //if (ExParameter.isCheat)
            //{
            //    StartCoroutine(waitForInit(uiSound));
            //}

            uiSoundObjects.Add(uiSound);

            nextButtonIndex++;
        }
    }

    //public void UISoundAction(int index)
    //{
    //    // 音源提示中の表示を解除
    //    isPresentPanel.SetActive(false);

    //    // 選択の処理
    //    soundManager.SoundAction(index);

    //    // NextPanelを表示
    //    nextPanel.SetActive(true);

    //    // 試行回数を満たしたらQuitButtonを表示
    //    if(ExParameter.maxTaskCount <= PresentInfo.exCount)
    //    {
    //        quitButton.SetActive(true);
    //    }

    //    Debug.Log(index);
    //}
}
