using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeDemoScene(string scenename)
    {
        if (DemoParameter.isDemo)
        {
            SceneManager.LoadScene("(12Pro)DemoScene");
        }
        else
        {
            SceneManager.LoadScene(scenename);
        }
    }
}