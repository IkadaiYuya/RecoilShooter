using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    //スコア
    public static int score = 0;
    //スコア倍率減衰時間カウンタ
    private float DicScoreMagnificationTimer = 180.0f;
    //スコア倍率減衰時間の最大フレーム数
    [SerializeField] private const float MaxDicScoreMagnificationTime = 180.0f;
    //スコア倍率
    private int scoreMagnification = 1;
    //最大スコア倍率
    [SerializeField] private int maxScoreMagni = 20;
    //UIでの倍率表示
    [SerializeField] private Text scoreMagni;
    //UIでのスコア表示
    [SerializeField] private Text totalScore;
    //UIでのスライダー表示
    [SerializeField] private Slider slider;
    //UIでのスコア倍率表示
    [SerializeField] private GameObject scoreMagniPanel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //スコア倍率がかかっていたら
        if (scoreMagnification > 1)
        {
            //スコア倍率パネルを表示
            scoreMagniPanel.SetActive(true);
            //スコア倍率減衰管理
            DicScoreMagni();
            //スライダーの表示更新
            slider.value = DicScoreMagnificationTimer / MaxDicScoreMagnificationTime;
        }
        //スコア倍率が等倍なら
        else
        {
            //スコア倍率パネルを非表示
            scoreMagniPanel.SetActive(false);
        }
        //表示スコアの更新
        totalScore.text = score.ToString();
        //表示スコア倍率の更新
        scoreMagni.text = scoreMagnification.ToString();
	}

    //スコア倍率減衰管理
    private void DicScoreMagni()
    {
        //カウンタからデルタタイムをフレームに直して減算
        DicScoreMagnificationTimer -= Time.deltaTime * 60.0f;
        //カウンタが0以下になったら
        if(DicScoreMagnificationTimer <= 0)
        {
            //倍率を1に戻す
            scoreMagnification = 1;
            DicScoreMagnificationTimer = MaxDicScoreMagnificationTime;
            Debug.Log("Magni_ReSet");
        }
    }

    //スコア加算用関数
    //引数:(敵の所持ポイント)
    public void AddScore(int point)
    {
        //現在のスコアにポイント×スコア倍率を加算
        score += point * scoreMagnification;
        //スコア倍率の加算
        scoreMagnification++;
        //スコア倍率が上限を超えたら
        if(scoreMagnification >= maxScoreMagni)
        {
            //スコア倍率を最大値に設定
            scoreMagnification = maxScoreMagni;
        }
        //スコア倍率の時間を最大値に戻す
        DicScoreMagnificationTimer = MaxDicScoreMagnificationTime;
    }
}
