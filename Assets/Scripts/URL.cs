using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URL : MonoBehaviour
{
    public void openURL()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScoNa47l-I1B6WtW0inyKc3eQvIExnNwHXhk8jjKfPRzYrOpQ/viewform?usp=sf_link");//""の中には開きたいWebページのURLを入力します
    }
}
