using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {

    //スコア表示用テキスト
    [SerializeField] private Text scoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //テキストに今回のスコアを設定
        scoreText.text = ScoreManager.score.ToString();	
	}
}
