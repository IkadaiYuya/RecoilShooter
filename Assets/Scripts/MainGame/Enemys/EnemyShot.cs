using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    //Bullet出現オブジェクト
    [SerializeField] private Transform launchPos;
    //敵の弾オブジェクト
    [SerializeField] private GameObject bullet;
    //弾の移動速度
    [SerializeField] private float shotsSpeed = 10.0f;
    //弾発射間隔
    [SerializeField] private float shotsTime = 2.0f;
    //弾発射カウンター
    private float timer = 0;
    //連発時の弾発射間隔
    [SerializeField] private float shotsInterval = 0.16f;
    //連発時のカウンタ
    private int shotsCnt = 0;
    //連発する弾の数
    [SerializeField] private int howManyFire = 3;
    //プレイヤーのオブジェクト
    [ExecuteInEditMode] public GameObject playerPos;
    //
    [SerializeField] private Vector3 distance;
    //プレイヤーの少し先の座標参照用
    private GameObject targetPos;

    // Use this for initialization
    void Start()
    {
        //プレイヤーの座標参照用
        playerPos = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーの少し先の座標参照用
        targetPos = GameObject.Find("TargetPos");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが生きているか
        if(playerPos != null)
        {//消滅していなかったら
            //時間になったら
            if (timer <= 0)
            {
                //弾発射
                Fire();
            }
            //タイマー減衰
            timer -= Time.deltaTime;
        }
    }

    //発射の種類選択
    private void Fire()
    {
        //ランダムで0 ~ 2を選出
        int ran = Random.Range(0, 2);
        switch (ran)
        {
            //2/3で
            case 0:
            case 1:
                //指定した数の弾を連射
                BurstFire();
                break;
            //1/3で
            case 2:
                //１発をプレイヤーに向けて発射
                TargetDirFire();
                break;
        }
    }

    //プレイヤーを狙う弾
    private void TargetDirFire()
    {
        //弾の生成
        GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        //方向と移動量の初期化
        Vector3 force = Vector3.zero;
        //自身の正面に移動量を設定
        //常にプレイヤーの方を向くため正面でプレイヤーに向かう
        force = this.gameObject.transform.forward * (shotsSpeed * 100.0f);
        //設定した移動量と方向に力を加える
        bullets.GetComponent<Rigidbody>().AddForce(force);
        //発射位置の設定
        bullets.transform.position = launchPos.position;
        //次の発射間隔を指定
        timer = shotsTime;
    }

    //プレイヤーの少し前に指定しただけ連射
    private void BurstFire()
    {
        //カウンタを加算
        shotsCnt++;
        //発射した数が指定した値より多いか
        if (shotsCnt < howManyFire)
        {//少なかったら
            //次の発射間隔を指定
            //通常より短い時間
            timer = shotsInterval;
        }
        else
        {//多かったら
            //次の発射間隔を指定
            timer = shotsTime;
            //連射数を初期化
            shotsCnt = 0;
        }
        //弾の生成
        GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        //発射角を初期化
        Vector3 force = Vector3.zero;
        //発射方向
        Vector3 forceDirection = Vector3.zero;

        //角度を算出
        force = (playerPos.transform.position + distance) - this.transform.position;
        //
        forceDirection = force.normalized;
        //force = new Vector3((playerPos.transform.position.x + targetPos.transform.localPosition.x) - this.transform.position.x, 0, (playerPos.transform.position.x + targetPos.transform.localPosition.z) - this.transform.position.z).normalized * (shotsSpeed * 100.0f);
        //設定した移動速度と方向に力を加える
        bullets.GetComponent<Rigidbody>().AddForce(forceDirection * (shotsSpeed * 100.0f));

        //発射位置の設定
        bullets.transform.position = launchPos.position;
    }
}
