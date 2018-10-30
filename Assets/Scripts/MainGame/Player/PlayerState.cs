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
    //HPのUI表示スクリプト
    private PlayerHPView viewer;

    private Camera camera;
    private CameraShake shake;
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
            //画面を揺らす
            shake.Shake(0.12f, 0.75f);
            //耐久力が0以下なら
            if (this.hp <= 0)
            {
                //消滅する前にHPのUIを非表示に
                viewer.HideHP();

                shake.Shake(1.75f, 1.5f);
                //削除
                Destroy(this.gameObject);
                //爆発エフェクト生成
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
            //画面を揺らす
            shake.Shake(0.12f, 0.75f);
            //敵の弾を削除
            Destroy(other.gameObject);
            //耐久力が0以下なら
            if (this.hp <= 0)
            {
                //消滅する前にHPのUIを非表示に
                viewer.HideHP();

                shake.Shake(1.75f, 1.5f);
                //削除
                Destroy(this.gameObject);
                //爆発エフェクト生成
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

        camera = Camera.main;
        shake = camera.GetComponent<CameraShake>();
    }
	
	// Update is called once per frame
	void Update () {
        //ダメージを受けていたら
        if(damage)
        {
            //HPのUIを表示
            //表示が終了するとfalseを返してくる
            //引数：(残りHP)
            damage = viewer.HPView(hp);
        }
	}
}
