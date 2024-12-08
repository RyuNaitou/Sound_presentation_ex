using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetSoundInfoManager : MonoBehaviour
{
    [Tooltip("�Ώۉ�����")] public TextMeshProUGUI targetInfoText;
    [Tooltip("�Ώۉ����̉摜")] public Image targetImage;

    [Tooltip("�������Ƃ̑Ή�")] Dictionary<SOUNDTYPE, string> soundTypeDic = new Dictionary<SOUNDTYPE, string>()
    {
        {SOUNDTYPE.XYLOPHONE, "�؋�" },
        {SOUNDTYPE.BELL, "��" },
        {SOUNDTYPE.CAT, "�L" },
        {SOUNDTYPE.PIANO, "�s�A�m" },
        {SOUNDTYPE.BUBBLE, "�A" },
        {SOUNDTYPE.TRUMPET, "�g�����y�b�g" },
        {SOUNDTYPE.DOG, "��" },
        {SOUNDTYPE.FLUTE, "�t���[�g" },
        {SOUNDTYPE.SEAGULL, "�J����" },
        {SOUNDTYPE.CORRECT, "������" },
    };

    public void changeTargetInfo(SoundData targetSound)
    {
        // �Ώۉ����̕\�����X�V

        if(targetInfoText != null)
        {
            targetInfoText.text = $"�Ώۉ���:\n{soundTypeDic[targetSound.type]}";
        }
        if(targetImage != null)
        {
            targetImage.sprite = targetSound.sprite;
        }
    }
}
