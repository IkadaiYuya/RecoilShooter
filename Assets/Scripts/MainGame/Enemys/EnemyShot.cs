using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {

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
    //
    private GameObject targetPos;

	// Use this for initialization
	void Start () {
        targetPos = GameObject.Find("TargetPos");
    }
	
	// Update is called once per frame
	void Update () {
        if(timer <= 0)
        {
            Fire();
        }
        timer -= Time.deltaTime;
    }

    //
    private void Fire()
    {
        int ran = Random.Range(0, 2);
        switch (ran)
        {
            case 0:
            case 1:
                BurstFire();
                break;
            case 2:
                TargetDirFire();
                break;
        }
    }

    //
    private void TargetDirFire()
    {
        GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Vector3 force = Vector3.zero;
        force = this.gameObject.transform.forward * (shotsSpeed * 100.0f);
        bullets.GetComponent<Rigidbody>().AddForce(force);
        bullets.transform.position = launchPos.position;
        timer = shotsTime;
    }

    //
    private void BurstFire()
    {
        shotsCnt++;
        if(shotsCnt < howManyFire)
        {
            timer = shotsInterval;
        }
        else
        {
            timer = shotsTime;
            shotsCnt = 0;
        }
        GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Vector3 force = Vector3.zero;
        force = new Vector3(targetPos.transform.position.x - this.transform.position.x, 0, targetPos.transform.position.z - this.transform.position.z).normalized * (shotsSpeed * 100.0f);
        bullets.GetComponent<Rigidbody>().AddForce(force);
        bullets.transform.position = launchPos.position;
    }
}
