using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeModeScene()
    {
        switch (ModeParameter.exMode)
        {
            case ExMODE.Ex:
                SceneManager.LoadScene("(12Pro)ExperimentScene2");
                break;
            case ExMODE.HTEx:
                SceneManager.LoadScene("(12Pro)HTExScene");
                break;
            case ExMODE.Demo:
                SceneManager.LoadScene("(12Pro)DemoScene");
                break;
        }
    }
}