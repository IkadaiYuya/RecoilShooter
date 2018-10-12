using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    //耐久力
    [SerializeField] private int hp = 1;
    //爆発のエフェクト
    [SerializeField] private GameObject bombEffect;
    //自身のスコア
    private int point = 100;
    //スコア管理スクリプト
    private ScoreManager scoreMane; 
    
    // Use this for initialization
    void Start () {
        scoreMane = GameObject.Find("MainGameUI").GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //当たり判定処理
    private void OnTriggerEnter(Collider other)
    {
        //当たったのが弾だったら
        if (other.gameObject.tag == "Bullet")
        {
            //耐久力を減らす
            this.hp--;
            //敵と当たっていたら弾を消滅させる
            Destroy(other.gameObject);
            //残り耐久力が0以下だったら
            if (this.hp <= 0)
            {
                //スコアを加算
                scoreMane.AddScore(this.point);
                //爆破エフェクトを生成
                Instantiate(bombEffect, this.transform.position, transform.rotation);
                //敵を消滅させる
                Destroy(this.gameObject);
            }
        }
        //プレイヤーだったら
        else if (other.gameObject.tag == "Player")
        {
            //耐久力を減らす
            this.hp--;
            //残り耐久力が0以下だったら
            if (this.hp <= 0)
            {
                //スコアを加算
                scoreMane.AddScore(this.point);
                //爆破エフェクトを生成
                Instantiate(bombEffect, this.transform.position, transform.rotation);
                //敵を消滅させる
                Destroy(this.gameObject);
            }
        }
    }
}
