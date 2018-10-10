using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    //耐久力
    [SerializeField] private int hp = 3;

    //あたり判定処理
    private void OnTriggerEnter(Collider other)
    {
        //当たったオブジェクトのタグが"Enemy"なら
        if (other.gameObject.tag == "Enemy")
        {
            //自身の耐久力を減衰
            this.hp--;
            //耐久力が0以下なら
            if(this.hp <= 0)
            {
                //削除
                Destroy(this.gameObject);
            }
        }
        //当たったオブジェクトのタグが"EnemyBullet"なら
        if (other.gameObject.tag == "EnemyBullet")
        {
            //自身の耐久力を減衰
            this.hp--;
            //敵の弾を削除
            Destroy(other.gameObject);
            Debug.Log("EnemyBullet_Hit");
            //耐久力が0以下なら
            if (this.hp <= 0)
            {
                //削除
                Destroy(this.gameObject);
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
