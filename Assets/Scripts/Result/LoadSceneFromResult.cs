using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromResult : MonoBehaviour {
    //
    private Select_Player_Result select;
    //
    private float timer = 0.0f;
    //
    [SerializeField] private float autoTitleLoadTime = 30.0f;
    //
    private FadeManager fmana;

    // Use this for initialization
    void Start()
    {
        fmana = GameObject.Find("Canvas").GetComponent<FadeManager>();
        fmana.FadeIn();
        select = GameObject.Find("Select_Player").GetComponent<Select_Player_Result>();
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //選択が終了していたら
        if (select.selected)
        {
            //Exitが選択されたら
            if(select.selected_exit)
            {
                //タイトルに戻る
                fmana.FadeOut("Title");
            }
            //プレイヤーが選択されたら
            else
            {
                //メインゲームに戻る
                fmana.FadeOut("MainGame");
            }
        }
        //一定時間選択されなかったら
        else if(timer > autoTitleLoadTime)
        {
            //タイトルに戻る
            fmana.FadeOut("Title");
        }
        timer += Time.deltaTime;
    }

    public void ResetAutoTimer()
    {
        timer = 0.0f;
    }
}
