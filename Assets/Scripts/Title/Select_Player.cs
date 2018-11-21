using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Player : MonoBehaviour {

    //機体の番号
    enum Select_Players
    {
        Normal, //通常機体
        Speed,  //
        Heavy,  //
    }
    //現在選んでいる機体
    private Select_Players playerNum = Select_Players.Normal;
    //選択時の回転速度
    [SerializeField] private float rotSpeed = 120.0f;
    //スティックの長押し非対応
    private bool stickDown = false;
    //選択したプレイヤー番号
    public static int selected_Player = 0;
    //入力値
    private Vector3 input = Vector3.zero;
    //選択し終わっているかどうか
    public bool selected = false;
    //回転先
    private Vector3[] rotAngle = new Vector3[3];

	// Use this for initialization
	void Start () {
        //回転先を初期化
        for (int pN = 0; pN < 3; ++pN)
        {
            //120度ごとに設定
            rotAngle[pN] = new Vector3(0, 120 * pN, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //入力処理
        Input_Stick();
        //入力に対応して回転
        Rotation_Selected();
        //選択処理
        Selected();
	}

    //入力判定処理
    private void Input_Stick()
    {
        //入力値を取得
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //入力値がX軸の方が大きいなら
        if (input.x > 0.1f && input.x > input.y ||
            input.x < -0.1f && input.x < input.y)
        {
            //Horizontal側の処理を行う
            Input_Horizontal();
        }
        //入力されていなかったら
        if (stickDown && input.magnitude == 0)
        {
            stickDown = false;
        }

        //入力されていなかったら
        if (stickDown && input.magnitude == 0)
        {
            stickDown = false;
        }
    }


    //左右の入力があった時の処理
    void Input_Horizontal()
    {
        //前のフレームで未入力で現在入力があるとき
        if (!stickDown)
        {
            //押されている
            stickDown = true;
            //右
            if (input.x > 0)
            {
                //各右のプレイヤに回転
                switch (playerNum)
                {
                    case Select_Players.Normal:
                        playerNum = Select_Players.Speed;
                        break;
                    case Select_Players.Speed:
                        playerNum = Select_Players.Heavy;
                        break;
                    case Select_Players.Heavy:
                        playerNum = Select_Players.Normal;
                        break;
                }
            }
            //左
            else
            {
                //各左のプレイヤに回転
                switch (playerNum)
                {
                    case Select_Players.Normal:
                        playerNum = Select_Players.Heavy;
                        break;
                    case Select_Players.Speed:
                        playerNum = Select_Players.Normal;
                        break;
                    case Select_Players.Heavy:
                        playerNum = Select_Players.Speed;
                        break;
                }
            }
        }
    }

    //回転
    private void Rotation_Selected()
    {
        //機体たちを選択されている機体が中央に来るように指定
        Quaternion angle = Quaternion.Euler(rotAngle[PlayerNum()]);
        //指定した向きまで回転
        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotSpeed * Time.deltaTime);
    }

    //プレイヤの列挙体をint型で返す
    private int PlayerNum()
    {
        //通常機体なら
        if(playerNum == Select_Players.Normal)
        {
            return 0;
        }
        //速度機体なら
        else if(playerNum == Select_Players.Speed)
        {
            return 1;
        }
        //盾形機体なら
        return 2;
    }

    //選択処理
    private void Selected()
    {
        //まだ選択されてないかつ選択入力が有ったら
        if(Input.GetButtonDown("Fire1") && !selected)
        {
            //現在の選択
            switch (playerNum)
            {
                case Select_Players.Normal:
                    selected_Player = 0;
                    break;
                case Select_Players.Speed:
                    selected_Player = 1;
                    break;
                case Select_Players.Heavy:
                    selected_Player = 2;
                    break;
            }
            //フラグをオンに
            selected = true;
        }
    }
}
