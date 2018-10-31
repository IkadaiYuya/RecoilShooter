using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGame : MonoBehaviour {

    //
    private Select_Player select;
    //
    private FadeManager fmana;

    // Use this for initialization
    void Start()
    {
        fmana = GameObject.Find("Canvas").GetComponent<FadeManager>();
        fmana.FadeIn();
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
        fmana.FadeOut("MainGame");
    }
}
