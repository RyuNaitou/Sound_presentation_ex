using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObjectManager : MonoBehaviour
{
    [Tooltip("���̉����̍���")] float pitchAngel;
    [Tooltip("���̉����̐��������̈ʒu")] float yawAngle;

    [Tooltip("���������̌����ʂ̏d��")] float weight = 1f;
    [Tooltip("�A�^�b�`���ꂽ����")] AudioSource audioSource;

    [Tooltip("���X�i�[�̌���")] public Transform listenerTransform;

    // Start is called before the first frame update
    void Start()
    {
        listenerTransform = GameObject.Find("/Listener").transform;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float volume = 1f;
        if(ExParameter.isHorizonalChangeVolume && PresentInfo.soundLineNumber == 1)
        {
            volume = volume * changeVolumefromYaw();
        }
        if(ExParameter.isVerticalChangeVolume && PresentInfo.soundLineNumber != 1)
        {
            volume = volume * changeVolumefromPitch();
        }

        audioSource.volume = volume;
    }

    float changeVolumefromPitch()
    {
        // ���X�i�[�̌����Ă��鍂���ɂ���ă{�����[����ύX

        float volume = 1f;

        if (pitchAngel != null)
        {
            // ���X�i�[�̃s�b�`���擾

            // �I�u�W�F�N�g�̃��[�J����]��Quaternion�Ŏ擾(Unity�̎g�p��90���ŉ��̂����]���邽�߁AQuaternion����v�Z)
            Quaternion rotation = listenerTransform.localRotation;

            // �s�b�`�p�x���v�Z�i�O���x�N�g������Z�o�j
            float listenerPitch = Mathf.Atan2(2f * (rotation.w * rotation.x + rotation.y * rotation.z),
                                      1f - 2f * (rotation.x * rotation.x + rotation.y * rotation.y)) * Mathf.Rad2Deg;

            //0~360�̂��߁A-180~180��
            if (listenerPitch > 180)
            {
                listenerPitch = listenerPitch - 360f;
            }
            listenerPitch = -listenerPitch; // ����肪���̂���

            //Debug.Log($"Pitch:{listenerPitch}");


            // �����̍����特�ʂ̑傫����ύX
            //volume = 1f - Mathf.Abs(listenerPitch - pitchAngel) / 180f;
            // �Ȃ��炩��
            float normalized4 = Mathf.Pow((listenerPitch - pitchAngel) / 180f, 4);
            volume = 1f - normalized4 / (0.003f + normalized4);

        }

        return volume;
    }
    float changeVolumefromYaw()
    {
        // ���X�i�[�̌����Ă��鐅�������̌����ɂ���ă{�����[����ύX

        float volume = 1f;

        if (yawAngle != null)
        {
            // ���X�i�[�̐��������̌������擾
            float listenerYaw = listenerTransform.eulerAngles.y;
            //0~360�̂��߁A-180~180��
            if(listenerYaw > 180)
            {
                listenerYaw = listenerYaw - 360f;
            }
            //Debug.Log($"Yaw:{listenerYaw}");

            // �����̍����特�ʂ̑傫����ύX
            volume = 1f - (Mathf.Abs(listenerYaw - yawAngle) / 120f) * weight;
        }

        return volume;
    }

    public void setAngles(float yawAngleValue, float pitchAngleValue)
    {
        yawAngle = yawAngleValue;
        pitchAngel = pitchAngleValue;
    }
}
