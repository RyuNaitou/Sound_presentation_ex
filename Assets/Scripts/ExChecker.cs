using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExChecker : MonoBehaviour
{
    // �������Ƃ̎����������������ǂ������`�F�b�N�}�[�N�ŕ\��
    // �`�F�b�N�}�[�N�ɃA�^�b�`

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
        // �����񎦊Ԋu
        int presentIntervalIndex = (int)(PresentInfo.exName) - 1;

        // ���I�ɕς���ꍇ�́A���݂̎�����񂩂�
        if(dynamic)
        {
            targetNumber = PresentInfo.soundNumber;
            targetLineNumber = PresentInfo.soundLineNumber;
        }

        // �z��̃C���f�b�N�X�ɍ��킹��
        targetNumber -= 4;
        targetLineNumber -= 1;

        checkMarkImage.enabled = PresentInfo.exFinished[presentIntervalIndex, targetNumber, targetLineNumber];
    }
}
