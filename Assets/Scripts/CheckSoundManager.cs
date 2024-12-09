using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSoundManager : MonoBehaviour
{
    public GameObject playingDisablePanel;
    public List<GameObject> disablePanels;

    public AudioSource thisSound;

    public SOUNDTYPE thisSoundType;

    public void checkSound()
    {
        // 押したボタンを緑に
        playingDisablePanel.SetActive(true);

        // 他のボタンを灰色に
        for (int i = 0; i < disablePanels.Count; i++)
        {
            // 押したボタンは無視
            if(i == (int)thisSoundType)
            {
                continue;
            }
            disablePanels[i].SetActive(true);
        }

        // 音源を再生
        thisSound.Play();

        // 音源の再生が終わったら、フィルムを外す
        StartCoroutine(waitForCheckingSound());
    }

    IEnumerator waitForCheckingSound()
    {
        // 音源が再生されている間は、ボタンのフィルムを表示
        while (thisSound.isPlaying)
        {
            yield return null;
        }

        // 押したボタンの緑のフィルムを外す
        playingDisablePanel.SetActive(false);

        // 他のボタンの灰色のフィルムを外す
        for (int i = 0; i < disablePanels.Count; i++)
        {
            // 押したボタンは無視
            if (i == (int)thisSoundType)
            {
                continue;
            }
            disablePanels[i].SetActive(false);
        }
    }
}
