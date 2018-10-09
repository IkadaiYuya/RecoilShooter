using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    //耐久力
    [SerializeField] private int hp = 1;

    private void OnTriggerEnter(Collider other)
    {
        //当たったのが弾だったら
        if (other.gameObject.tag == "Bullet")
        {
            //ログに当たったことを残す
            Debug.Log("Enemy_Bullet_Hit");
            //耐久力を減らす
            this.hp--;
            //残り耐久力が0以下だったら
            if (this.hp <= 0)
            {
                //敵を消滅させる
                Destroy(this.gameObject);
                //ログに敵が消滅したことを残す
                Debug.Log("Enemy_Destroy");
            }
            //敵と当たっていたら弾を消滅させる
            Destroy(other.gameObject);
        }
        //プレイヤーだったら
        if (other.gameObject.tag == "Player")
        {
            //ログに当たったことを残す
            Debug.Log("Enemy_Player_Hit");
            //耐久力を減らす
            this.hp--;
            //残り耐久力が0以下だったら
            if (this.hp <= 0)
            {
                //敵を消滅させる
                Destroy(this.gameObject);
                //ログに敵が消滅したことを残す
                Debug.Log("Enemy_Destroy");
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
