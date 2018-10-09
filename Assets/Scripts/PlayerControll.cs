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
		rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (moveDirection.magnitude != 0) {
			rotAngle = Quaternion.LookRotation(moveDirection);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotAngle, rotSpeed * Time.deltaTime);
		}

		if (Input.GetButton("Fire1")) {
			Fire();
			Move();
		}
        //タイマーの減衰
        timer -= Time.deltaTime;
    }

	void Fire()
	{
        if(ShotTimer())//発射間隔
        {
		    GameObject bullets = GameObject.Instantiate(bullet, transform.position, transform.rotation) as GameObject;
		    Vector3 force;
		    force = this.gameObject.transform.forward * (shotSpeed * 100);
		    bullets.GetComponent<Rigidbody>().AddForce(force);
		    bullets.transform.position = launchPos.position;
        }
	}
	void Move()
	{
		rigidbody.AddForce(transform.forward * -recoilPower, ForceMode.Impulse);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
	}

    //
    private bool ShotTimer()
    {
        if(timer <= 0.0f)
        {
            timer = time;
            return true;
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "StageLimitR")
        {
            rigidbody.AddForce(Vector3.left * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        if (other.gameObject.tag == "StageLimitL")
        {
            rigidbody.AddForce(Vector3.right * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        if (other.gameObject.tag == "StageLimitT")
        {
            rigidbody.AddForce(Vector3.back * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        if (other.gameObject.tag == "StageLimitB")
        {
            rigidbody.AddForce(Vector3.forward * boundPower);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
        Debug.Log("StageLimit_Hit");
    }
}
