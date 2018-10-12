using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    //敵の状態
    enum EnemyType
    {
        UnKnown = -1,
        Random,
        DirPlayer,
    };
    //現在の敵の状態
    private EnemyType eType = EnemyType.Random;
    //行動変更までの時間最大値
    [SerializeField] private float maxChengeTime = 10.0f;
    //行動変更までの時間最小値
    [SerializeField] private float minChengeTiem = 5.0f;
    //行動変更までのタイマー
    private float actionChengeTimer = 0.0f;
    //最大移動速度
    [SerializeField]private float MaxMoveSpeed = 4.5f;
    //移動速度
    [SerializeField] private float moveSpeed = 5.0f;
    //移動方向
    private Quaternion rotAngle;
    //回転速度
    [SerializeField] private float rotSpeed = 360.0f;
    //移動量
    private Vector3 moveDirection;
    //プレイヤー情報
    /*[SerializeField] */private Transform playerpos;
    //
    Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        playerpos = transform.Find("/Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        actionChengeTimer -= Time.deltaTime;
        ChengeType();
        AngleChenge();
        Move();
	}

    //向きなどの変更
    private void AngleChenge()
    {
        //プレイヤーに向きの計算
        rotAngle = Quaternion.LookRotation(playerpos.position - transform.position);
        //少しずつ向きを変更
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotAngle, rotSpeed * Time.deltaTime);
    }

    //移動処理
    private void Move()
    {
        switch (eType)
        {            
            case EnemyType.Random:
                //Thinkで計算した向きに移動
                rigidbody.AddForce(moveDirection);
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxMoveSpeed);
                //transform.Translate(transform.InverseTransformDirection(moveDirection) * moveSpeed * Time.deltaTime, Space.Self);
                break;
            case EnemyType.DirPlayer:
                //自身の向きに移動
                moveDirection = new Vector3(playerpos.position.x - transform.position.x, 0, playerpos.position.z - transform.position.z);
                rigidbody.AddForce(moveDirection);
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxMoveSpeed);
                //transform.Translate(transform.InverseTransformDirection(moveDirection) * moveSpeed * Time.deltaTime, Space.Self);
                break;
            default:
                break;
        }
    }

    //状態変更タイミング判定
    private bool ActionChengeTime()
    {
        if(actionChengeTimer <= 0.0f)
        {
            return true;
        }
        return false;
    }

    //状態変更
    private void ChengeType()
    {
        //変更のタイミングになったら
        if (ActionChengeTime())
        {
            //次の変更までの時間を指定
            actionChengeTimer = Random.Range(minChengeTiem, maxChengeTime);
            //状態変更処理
            int tmpEtype = Random.Range(0, 10) % 2;
            switch (tmpEtype)
            {
                case 0:
                    //状態を指定
                    eType = EnemyType.Random;
                    moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                    //前の状態での移動量をなくす
                    rigidbody.velocity = Vector3.zero;
                    break;
                case 1:
                    //状態を指定
                    eType = EnemyType.DirPlayer;
                    //少し長めにタイマーを伸ばす
                    actionChengeTimer += 6.0f;
                    //前の状態での移動量をなくす
                    rigidbody.velocity = Vector3.zero;
                    break;
            }
        }

    }
}
