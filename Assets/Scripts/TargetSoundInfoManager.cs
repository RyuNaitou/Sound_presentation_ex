using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetSoundInfoManager : MonoBehaviour
{
    [Tooltip("対象音源名")] public TextMeshProUGUI targetInfoText;
    [Tooltip("対象音源の画像")] public Image targetImage;

    [Tooltip("音源名との対応")]
    Dictionary<SOUNDTYPE, string> soundTypeDic = new Dictionary<SOUNDTYPE, string>()
    {
        {SOUNDTYPE.XYLOPHONE, "木琴" },
        {SOUNDTYPE.BELL, "鈴" },
        {SOUNDTYPE.CAT, "猫" },
        {SOUNDTYPE.PIANO, "ピアノ" },
        {SOUNDTYPE.BUBBLE, "泡" },
        {SOUNDTYPE.TRUMPET, "トランペット" },
        {SOUNDTYPE.DOG, "犬" },
        {SOUNDTYPE.FLUTE, "フルート" },
        {SOUNDTYPE.SEAGULL, "カモメ" },
        {SOUNDTYPE.CORRECT, "正解音" },
    };

    public void changeTargetInfo(SoundData targetSound)
    {
        // 対象音源の表示を更新

        if (targetInfoText != null)
        {
            targetInfoText.text = $"対象音源:\n{soundTypeDic[targetSound.type]}";
        }
        if (targetImage != null)
        {
            targetImage.sprite = targetSound.sprite;
        }
    }
}