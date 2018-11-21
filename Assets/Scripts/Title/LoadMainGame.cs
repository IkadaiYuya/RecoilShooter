using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGame : MonoBehaviour {

    //機体の選択参照用
    private Select_Player select;
    //フェードマネージャー
    private FadeManager fmana;

    // Use this for initialization
    void Start()
    {
        //フェードマネージャーを取得
        fmana = GameObject.Find("Canvas").GetComponent<FadeManager>();
        //フェードイン開始
        fmana.FadeIn();
        //プレイヤー選択スクリプトの取得
        select = GameObject.Find("Select_Player").GetComponent<Select_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが選択されていたら
        if (select.selected)
        {
            //シーンを変更
            SceneChange();
        }
    }

    //シーン変更
    private void SceneChange()
    {
        //フェードアウト(次のシーン名)
        fmana.FadeOut("MainGame");
    }
}
