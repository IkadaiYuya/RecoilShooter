using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
	[SerializeField] private float rotSpeed = 360.0f;       //旋回速度

	private Quaternion rotAngle;
	private Vector3 moveDirection;

	[SerializeField] private float recoilPower = 2.0f;              //反動
	[SerializeField] private float maxSpeed = 5.0f;                 //最大加速度
    [SerializeField] private float boundPower = 1.0f;               //壁からの弾かれる強さ

	[SerializeField] private Transform launchPos;                   //Bullet出現オブジェクト
	[SerializeField] private GameObject bullet;                     //弾
	[SerializeField] private float shotSpeed = 20.0f;               //弾速
    [SerializeField] private float time = 0.3f;                     //弾の出現間隔初期値
    private float timer = 0.0f;                                     //弾の出現間隔タイマー
    
	Rigidbody rigidbody;

	void Start()
	{
        //rigidbodyを取得
		rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
        //プレイヤーの向きを入力値から指定
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (moveDirection.magnitude != 0) {//入力値が0でなかったら
            //回転量を向きからQuaternionで取得
			rotAngle = Quaternion.LookRotation(moveDirection);
            //回転
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotAngle, rotSpeed * Time.deltaTime);
		}
        //Fire1が押されたら
		if (Input.GetButton("Fire1")) {
            //弾発射処理
			Fire();
            //移動処理
			Move();
		}
        //タイマーの減衰
        timer -= Time.deltaTime;
    }

    //弾発射処理
	void Fire()
	{
        if(ShotTimer())//発射間隔判定
        {
		    GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
		    Vector3 force;
		    force = this.gameObject.transform.forward * (shotSpeed * 100);
		    bullets.GetComponent<Rigidbody>().AddForce(force);
		    bullets.transform.position = launchPos.position;
        }
	}
    //移動処理
	void Move()
	{
		rigidbody.AddForce(transform.forward * -recoilPower, ForceMode.Impulse);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
	}

    //弾発射間隔判定処理
    private bool ShotTimer()
    {
        //タイマーが0以下になったら
        if(timer <= 0.0f)
        {
            //次の発射までの時間を指定
            timer = time;
            //発射
            return true;
        }
        //まだ撃ってない
        return false;
    }

    //あたり判定処理
    private void OnTriggerStay(Collider other)
    {
        //ステージの
        //右端なら
        if(other.gameObject.tag == "StageLimitR")
        {
            //左方向(-X)に移動量を加える
            rigidbody.AddForce(Vector3.left * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        //左端なら
        if (other.gameObject.tag == "StageLimitL")
        {
            //右方向(+X)に移動量を加える
            rigidbody.AddForce(Vector3.right * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        //上底なら
        if (other.gameObject.tag == "StageLimitT")
        {
            //下方向(-Z)に移動量を加える
            rigidbody.AddForce(Vector3.back * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        //下底なら
        if (other.gameObject.tag == "StageLimitB")
        {
            //上方向(+Z)に移動量を加える
            rigidbody.AddForce(Vector3.forward * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
    }
}
