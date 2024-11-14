using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URL : MonoBehaviour
{
    public void openURL()
    {
        Application.OpenURL("https://getabakoclub.com/");//""の中には開きたいWebページのURLを入力します
    }
}
