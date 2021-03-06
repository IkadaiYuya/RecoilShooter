﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    //プレイヤー消滅後待機時間
    [SerializeField] private float waitTime = 3.0f;
    //フェードマネージャー用変数
    private FadeManager fmana;

	// Use this for initialization
	void Start () {
        //フェードマネージャーを取得
        fmana = GameObject.Find("Canvas").GetComponent<FadeManager>();
        //フェードイン開始
        fmana.FadeIn();
    }

    // Update is called once per frame
    void Update () {
        //プレイヤーが消滅しているか
        if(PlayerIsDead())
        {//していたら
            //エネミーの消滅
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject ene in enemys)
            {
                Destroy(ene);
            }
            //リザルトSceneへ
            fmana.FadeOut("Results");
        }
	}

    //プレイヤーの消滅確認と一定時間の停止
    private bool PlayerIsDead()
    {
        //タグがプレイヤーのものを検索
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        //配列の長さが0以下(基本は0のみ)か判断
        if(player.Length <= 0 )
        {//0以下なら
            //待機時間を減衰
            waitTime -= Time.deltaTime;
            //時間になったら
            if(waitTime <= 0)
            {
                return true;
            }
        }
        return false;
    }
}
