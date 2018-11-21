using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    //フェードインアウトに使用するイメージ
    private Image image;
    //イメージの不透明度
    private float alpha;
    //フェードインのフラグ
    private bool fadeInFlag = false;
    //フェードアウトのフラグ
    private bool fadeOutFlag = false;
    //フェードインアウトにかける時間
    [SerializeField] private float fadeTime = 0.5f;
    //次のシーンの名前
    private string nextScene = "";

    void Start()
    {
        //フェードインアウトに使うイメージを取得
        image = GameObject.Find("FadeImage").GetComponent<Image>();
    }

    //フェードインフラグをオンにする
    public void FadeIn()
    {
        //フラグがどちらもオフなら
        if (!fadeInFlag && !fadeOutFlag)
        {
            //フェードインのフラグをオンに
            fadeInFlag = true;
            //不透明度を最大に
            alpha = 1.0f;
        }
    }

    //フェードアウトフラグをオンにする
    //引数:(string型 次のシーンの名前)
    public void FadeOut(string nextS)
    {
        //フラグがどちらもオフなら
        if (!fadeInFlag && !fadeOutFlag)
        {
            //次のシーン名を保存
            nextScene = nextS;
            //フェードアウトフラグをオンに
            fadeOutFlag = true;
            //不透明度を最小に
            alpha = 0.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //フェードインフラグがオンなら
		if(fadeInFlag)
        {
            //不透明度を指定した時間で0になるように減算
            alpha -= Time.deltaTime / fadeTime;

            //不透明度が0以下になったら
            if(alpha <= 0)
            {
                //フラグをオフに
                fadeInFlag = false;
                //不透明度がマイナスにならないように0に初期化
                alpha = 0.0f;
            }
            //イメージの不透明度を更新
            image.color = new Color(0, 0, 0, alpha);
        }
        //フェードアウトフラグがオンなら
        else if(fadeOutFlag)
        {
            //不透明度を指定した時間で1になるように加算
            alpha += Time.deltaTime / fadeTime;

            //不透明度が1以上になったら
            if(alpha >= 1)
            {
                //フラグをオフに
                fadeOutFlag = false;
                //不透明度が1以上にならないように1に指定
                alpha = 1.0f;
                //次のシーンがタイトルかメインだったら
                //(メインからリザルトへ行くとき以外)
                if(nextScene == "Title" || nextScene == "MainGame")
                {
                    //スコアの初期化
                    ScoreManager.score = 0;
                }
                //次のシーン呼び出し
                SceneManager.LoadScene(nextScene);
            }

            //イメージの不透明度を更新
            image.color = new Color(0, 0, 0, alpha);
        }
    }
}
