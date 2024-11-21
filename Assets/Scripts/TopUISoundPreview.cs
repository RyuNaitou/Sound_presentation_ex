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
        // �ォ�猩�������z�u�v���r���[�𐶐�(��ԉ����������s)

        // ��ԉ����������s�̉��������擾
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

        // �ő�p�x�Ԋu�ŕ��ׂĂ��A180���ȓ��Ɏ��܂邩
        float angleInterval = (soundManager.maxAngleInterval * maxSoundCount <= 180f) ? soundManager.maxAngleInterval : 180f / (maxSoundCount - 1);

        // ���[�̃v���r���[�̊p�x���v�Z
        float startAngle = 180f - (angleInterval * (maxSoundCount - 1)) / 2f;

        for (int i = 0; i < maxSoundCount; i++)
        {
            float angle = startAngle + angleInterval * i;

            float x = -Mathf.Sin(Mathf.Deg2Rad * angle) * radius; // ������Ȃ̂Ń}�C�i�X
            float y = -Mathf.Cos(Mathf.Deg2Rad * angle) * radius + anchorPosY;

            // �v���r���[�C���X�^���X����
            GameObject topUISoundPreview =  Instantiate(TopUISoundPreviewPrefab, new Vector3(x, y, 0), Quaternion.identity);
            topUISoundPreview.transform.SetParent(transform, false);
        }
    }
}
