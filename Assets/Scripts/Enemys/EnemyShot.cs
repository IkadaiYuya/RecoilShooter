using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {

    //Bullet出現オブジェクト
    [SerializeField] private Transform launchPos;
    //敵の弾オブジェクト
    [SerializeField] private GameObject bullet;
    //弾の移動速度
    [SerializeField] private float shotSpeed = 100.0f;
    //弾発射間隔
    [SerializeField] private float shotsTime = 2.0f;
    //弾発射カウンター
    private float timer = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(shotsTime - timer <= 0)
        {
            Fire();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void Fire()
    {
        GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Vector3 force;
        force = this.gameObject.transform.forward * (shotSpeed * 10.0f);
        bullets.GetComponent<Rigidbody>().AddForce(force);
        bullets.transform.position = launchPos.position;
    }
}
