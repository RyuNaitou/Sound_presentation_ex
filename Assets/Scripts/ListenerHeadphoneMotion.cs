using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HearXR;

public static class ListenerHeadphoneInfo
{
    public static Quaternion _calibratedOffset = Quaternion.identity;
    public static bool inited = false;
}

public class ListenerHeadphoneMotion : MonoBehaviour
{
    // Airpods Proで動かすオブジェクトにアタッチ

    private Quaternion _lastRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        if (!ListenerHeadphoneInfo.inited)
        {
            // This call initializes the native plugin.
            Debug.Log("Before HeadphoneMotion.Init()");
            HeadphoneMotion.Init();
            Debug.Log("After HeadphoneMotion.Init()");

            // Check if headphone motion is available on this device.
            if (HeadphoneMotion.IsHeadphoneMotionAvailable())
            {
                // Subscribe to the rotation callback.
                // Alternatively, you can subscribe to OnHeadRotationRaw event to get the 
                // x, y, z, w values as they come from the API.
                HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotationQuaternion;

                // Start tracking headphone motion.
                ToggleTracking(true);
                //HeadphoneMotion.StartTracking();
            }

            ListenerHeadphoneInfo.inited = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleHeadRotationQuaternion(Quaternion rotation)
    {
        // Match the target object's rotation to the headphone rotation.
        if (ListenerHeadphoneInfo._calibratedOffset == Quaternion.identity)
        {
            this.transform.rotation = rotation;
        }
        else
        {
            this.transform.rotation = rotation * Quaternion.Inverse(ListenerHeadphoneInfo._calibratedOffset);
        }

        _lastRotation = rotation;
    }

    public void CalibrateStartingRotation()
    {
        // 正面に調整する
        ListenerHeadphoneInfo._calibratedOffset = _lastRotation;
    }

    public void ToggleTracking(bool status)
    {
        if (status)
        {
            Debug.Log("Before StartTracking");
            HeadphoneMotion.StartTracking();
            Debug.Log("After StartTracking");
        }
        else
        {
            Debug.Log("Before StopTracking");
            //HeadphoneMotion.StopTracking();
            Debug.Log("After StopTracking");
        }
    }

}
