using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExDisablePanelManager : MonoBehaviour
{
    public GameObject disablePanel;

    // Update is called once per frame
    void Update()
    {
        if(PresentInfo.soundLineNumber == 1)
        {
            disablePanel.SetActive(true);
        }else if(PresentInfo.soundLineNumber == 2 && ((7 <= PresentInfo.soundNumber) && (PresentInfo.soundNumber <= 10)))
        {
            disablePanel.SetActive(true);
        }
        else if(PresentInfo.soundLineNumber == 3 && ((4 <= PresentInfo.soundNumber)&&(PresentInfo.soundNumber <= 6)))
        {
            disablePanel.SetActive(true);
        }
        else
        {
            disablePanel.SetActive(false);
        }
    }
}
