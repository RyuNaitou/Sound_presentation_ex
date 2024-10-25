using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int exCount = -1;
        this.GetComponent<TextMeshProUGUI>().text = exCount.ToString() + "回目";
    }
}
