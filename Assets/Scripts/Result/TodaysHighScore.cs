using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TodaysHighScore : MonoBehaviour {

    //ハイスコア
    private int highScore = 0;
    //ハイスコア表示用テキスト
    [SerializeField] private Text ths;

	// Use this for initialization
	void Start () {
        //今日の保存先があるか確認
        if(!PlayerPrefs.HasKey("Score" + DateTime.Today.ToBinary().ToString()))
        {//なければ
            //一旦すべてを削除
            PlayerPrefs.DeleteAll();
            //初期化
            PlayerPrefs.SetInt("Score" + DateTime.Today.ToBinary().ToString(), 0);
        }
        //ハイスコアを読み込み
        highScore = PlayerPrefs.GetInt("Score" + DateTime.Today.ToBinary().ToString());
        //今回のスコアがハイスコアより多ければ
        if (ScoreManager.score > highScore)
        {
            //ハイスコアを更新
            highScore = ScoreManager.score;
            //今回のを保存
            PlayerPrefs.SetInt("Score" + DateTime.Today.ToBinary().ToString(), highScore);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //ハイスコアをテキストに更新
        ths.text = highScore.ToString();
    }
}
