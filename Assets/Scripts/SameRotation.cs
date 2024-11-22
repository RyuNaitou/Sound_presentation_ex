using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameRotation : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // 対象オブジェクトの回転（Euler角度）を取得
            Vector3 targetRotationEuler = target.rotation.eulerAngles;

            //// y軸の補正（180°ずれを修正）
            //targetRotationEuler.y -= 180f;

            // 修正後の回転を適用
            transform.rotation = Quaternion.Euler(targetRotationEuler);
        }
    }
}
