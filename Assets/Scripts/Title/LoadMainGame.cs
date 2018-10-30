using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGame : MonoBehaviour {

    //
    private Select_Player select;

    // Use this for initialization
    void Start()
    {
        FadeManager.FadeIn();
        select = GameObject.Find("Select_Player").GetComponent<Select_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (select.selected)
        {
            SceneChange();
        }
    }

    //
    private void SceneChange()
    {
        FadeManager.FadeOut();
        SceneManager.LoadScene("MainGame");
    }
}
