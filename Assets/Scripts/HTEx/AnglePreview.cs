using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnglePreview : MonoBehaviour
{
    public enum ANGLETYPE
    {
        YAW,
        PITCH,
    }
    public TextMeshProUGUI thisText;
    public Transform listenerTransform;

    public ANGLETYPE angleType;

    public static Vector3 GetEulerAnglesSignedStable(Transform t)
    {
        Vector3 angles = t.localEulerAngles;
        return new Vector3(NormalizeAngle(angles.x), NormalizeAngle(angles.y), NormalizeAngle(angles.z));
    }

    private static float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 listenerAngles = GetEulerAnglesSignedStable(listenerTransform);

        switch (angleType)
        {
            case ANGLETYPE.YAW:
                thisText.text = listenerAngles.y.ToString("N0") + "°";
                break;
            case ANGLETYPE.PITCH:
                thisText.text = listenerAngles.x.ToString("N0") + "°";
                break;
        }
    }
}