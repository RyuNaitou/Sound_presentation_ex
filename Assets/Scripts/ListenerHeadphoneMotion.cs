using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HearXR;

public class ListenerHeadphoneMotion : MonoBehaviour
{
    // Airpods Proで動かすオブジェクトにアタッチ

    private Quaternion _lastRotation = Quaternion.identity;
    private Quaternion _calibratedOffset = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        // This call initializes the native plugin.
        HeadphoneMotion.Init();

        // Check if headphone motion is available on this device.
        if (HeadphoneMotion.IsHeadphoneMotionAvailable())
        {
            // Subscribe to the rotation callback.
            // Alternatively, you can subscribe to OnHeadRotationRaw event to get the 
            // x, y, z, w values as they come from the API.
            HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotationQuaternion;

            // Start tracking headphone motion.
            HeadphoneMotion.StartTracking();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleHeadRotationQuaternion(Quaternion rotation)
    {
        // Match the target object's rotation to the headphone rotation.
        if (_calibratedOffset == Quaternion.identity)
        {
            this.transform.rotation = rotation;
        }
        else
        {
            this.transform.rotation = rotation * Quaternion.Inverse(_calibratedOffset);
        }

        _lastRotation = rotation;
    }

    public void CalibrateStartingRotation()
    {
        // 正面に調整する
        _calibratedOffset = _lastRotation;
    }

    public void ToggleTracking(bool status)
    {
        if (status)
        {
            HeadphoneMotion.StartTracking();
        }
        else
        {
            HeadphoneMotion.StopTracking();
        }
    }

}
