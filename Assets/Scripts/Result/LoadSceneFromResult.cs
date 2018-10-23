using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromResult : MonoBehaviour {
    //
    private Select_Player_Result select;
    //

    // Use this for initialization
    void Start()
    {
        select = GameObject.Find("Select_Player").GetComponent<Select_Player_Result>();
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
                SceneManager.LoadScene("Title");
            }
            //プレイヤーが選択されたら
            else
            {
                //メインゲームに戻る
                SceneManager.LoadScene("MainGame");
            }
        }
    }
}
