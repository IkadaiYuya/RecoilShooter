using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Player_Result : MonoBehaviour {

    //選択の状態
    enum Select_Option
    {
        Player_Select,  //プレイヤー選択
        Exit,           //タイトルへ
    }
    //初期選択状態
    private Select_Option option = Select_Option.Player_Select;
    //機体の番号
    enum Select_Players
    {
        Normal,
        Speed,
        Heavy,
    }
    //現在選んでいる機体
    private Select_Players playerNum = Select_Players.Normal;
    //選択時の回転速度
    [SerializeField] private float rotSpeed = 120.0f;
    //スティックの長押し非対応
    private bool stickDown = false;
    //選択したプレイヤー番号
    public static int selected_Player = 0;
    //LStick入力値
    private Vector2 input = Vector2.zero;
    //選択し終わっているかどうか
    [HideInInspector] public bool selected = false;
    //
    [HideInInspector] public bool selected_exit = false;
    //回転先
    private Vector3[] rotAngle = new Vector3[3];
    //
    [SerializeField] private List<GameObject> select;

    // Use this for initialization
    void Start () {
        //回転先を初期化
        for (int pN = 0; pN < 3; ++pN)
        {
            //120度ごとに設定
            rotAngle[pN] = new Vector3(0, 120 * pN, 0);
        }
        select[1].SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //入力処理
        Input_Stick();
        //入力に対応して回転
        Rotation_Selected();
        //UIの選択
        Select_UI();
        //選択処理
        Selected();
    }
    //入力判定処理
    private void Input_Stick()
    {
        //入力値を取得
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //入力値がX軸の方が大きいなら
        if(input.x > 0.1f && input.x > input.y ||
            input.x < -0.1f && input.x < input.y)
        {
            //Horizontal側の処理を行う
            Input_Horizontal();
        }
        //Y軸の方が大きかったら
        else if (input.y > 0.1f && input.x < input.y ||
            input.y < -0.1f && input.x > input.y)
        {
            Input_Vertical();
        }
        //入力されていなかったら
        if (stickDown && input.magnitude == 0)
        {
            stickDown = false;
        }
    }

    //Y軸入力対応
    void Input_Vertical()
    {
        //前のフレームで未入力なら
        if(!stickDown)
        {
            //押されている
            stickDown = true;
            //入力があったら逆にする
            switch (option)
            {
                case Select_Option.Player_Select:
                    option = Select_Option.Exit;
                    selected_exit = true;
                    break;
                case Select_Option.Exit:
                    option = Select_Option.Player_Select;
                    selected_exit = false;
                    break;
            }
        }
    }

    //X軸入力対応
    void Input_Horizontal()
    {
        //前のフレームで未入力でプレイヤー選択状態なら
        if (!stickDown && !selected_exit)
        {
            //押されている
            stickDown = true;
            //右
            if (input.x >0)
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
        Quaternion angle = Quaternion.Euler(rotAngle[PlayerNum()]);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotSpeed * Time.deltaTime);
    }

    //プレイヤの列挙体をint型で返す
    private int PlayerNum()
    {
        //通常機体なら
        if (playerNum == Select_Players.Normal)
        {
            return 0;
        }
        //速度機体なら
        else if (playerNum == Select_Players.Speed)
        {
            return 1;
        }
        //盾形機体なら
        return 2;
    }

    //
    private void Select_UI()
    {
        //プレイヤー選択中なら
        if(!selected_exit)
        {
            select[0].SetActive(true);
            select[1].SetActive(false);
        }
        else
        {
            select[0].SetActive(false);
            select[1].SetActive(true);
        }
    }

    //選択処理
    private void Selected()
    {
        //まだ選択されてないかつ選択入力が有ったら
        if (Input.GetButtonDown("Fire1") && !selected)
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
            selected = true;
        }
    }
}
