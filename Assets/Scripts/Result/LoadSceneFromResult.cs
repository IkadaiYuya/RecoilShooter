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

    // Use this for initialization
    void Start()
    {
        FadeManager.FadeIn();
        select = GameObject.Find("Select_Player").GetComponent<Select_Player_Result>();
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //選択が終了していたら
        if (select.selected)
        {
            //スコアを初期化
            ScoreManager.score = 0;
            //Exitが選択されたら
            if(select.selected_exit)
            {
                FadeManager.FadeOut();
                //タイトルに戻る
                SceneManager.LoadScene("Title");
            }
            //プレイヤーが選択されたら
            else
            {
                FadeManager.FadeOut();
                //メインゲームに戻る
                SceneManager.LoadScene("MainGame");
            }
        }
        //一定時間選択されなかったら
        else if(timer > autoTitleLoadTime)
        {
            FadeManager.FadeOut();
            //タイトルに戻る
            SceneManager.LoadScene("Title");
        }
        timer += Time.deltaTime;
    }

    public void ResetAutoTimer()
    {
        timer = 0.0f;
    }
}
