using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {

    //耐久力
    [SerializeField] private int hp = 3;
    //爆発エフェクト
    [SerializeField] private GameObject[] effects = new GameObject[3];
    //ダメージを受けたか
    private bool damage = false;
    //
    private PlayerHPView viewer;

    //あたり判定処理
    private void OnTriggerEnter(Collider other)
    {
        //当たったオブジェクトのタグが"Enemy"なら
        if (other.gameObject.tag == "Enemy")
        {
            //自身の耐久力を減衰
            this.hp--;
            //ダメージを通知
            damage = true;
            //耐久力が0以下なら
            if(this.hp <= 0)
            {
                //HPのUIを見えなくする
                viewer.LostHPView();
                //削除
                Destroy(this.gameObject);

                for(int e = 0; e < effects.Length; ++e)
                {
                    Instantiate(effects[e], transform.position, transform.rotation);
                }
            }
        }
        //当たったオブジェクトのタグが"EnemyBullet"なら
        if (other.gameObject.tag == "EnemyBullet")
        {
            //自身の耐久力を減衰
            this.hp--;
            //ダメージを通知
            damage = true;
            //敵の弾を削除
            Destroy(other.gameObject);
            Debug.Log("EnemyBullet_Hit");
            //耐久力が0以下なら
            if (this.hp <= 0)
            {
                //HPのUIを見えなくする
                viewer.LostHPView();
                //削除
                Destroy(this.gameObject);

                for (int e = 0; e < effects.Length; ++e)
                {
                    Instantiate(effects[e], transform.position, transform.rotation);
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        //HPのUI描画用スクリプトの所得
        viewer = GameObject.Find("HPViews").GetComponent<PlayerHPView>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hp > 1 && damage)
        {
            //HP表示関数に渡す
            damage = viewer.HPView(hp);
        }
        if(hp == 1)
        {
            viewer.LastHPView();
        }
	}

}
