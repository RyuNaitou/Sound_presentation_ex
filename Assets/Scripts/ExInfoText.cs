using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExInfoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() => this.GetComponent<TextMeshProUGUI>().text = "音源数選択(" + PresentInfo.exName + ")";
}
