using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedMarkerPre : MonoBehaviour
{
    public int exNum;

    public Sprite doneImage;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < PresentInfo.exFinished.GetLength(1); i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!PresentInfo.exFinished[exNum-1, i, j])
                {
                    return;
                }
            }
        }
        this.GetComponent<Image>().sprite = doneImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
