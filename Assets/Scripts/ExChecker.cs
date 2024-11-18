using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExChecker : MonoBehaviour
{
    // 条件ごとの実験が完了したかどうかをチェックマークで表示
    // チェックマークにアタッチ

    public bool dynamic;
    public int targetNumber;
    public int targetLineNumber;

    Image checkMarkImage;

    // Start is called before the first frame update
    void Start()
    {
        checkMarkImage = this.GetComponent<Image>();
        checkMarkImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 音源提示間隔
        int presentIntervalIndex = (int)(PresentInfo.exName) - 1;

        // 動的に変える場合は、現在の実験情報から
        if(dynamic)
        {
            targetNumber = PresentInfo.soundNumber;
            targetLineNumber = PresentInfo.soundLineNumber;
        }

        // 配列のインデックスに合わせる
        targetNumber -= 4;
        targetLineNumber -= 1;

        checkMarkImage.enabled = PresentInfo.exFinished[presentIntervalIndex, targetNumber, targetLineNumber];
    }
}
