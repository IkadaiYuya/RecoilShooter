using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Player : MonoBehaviour {

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
    //
    private bool stickDown = false;
    //
    public static int selected_Player = 0;
    //
    public bool selected = false;
    //
    private Vector3[] rotAngle = new Vector3[3];

	// Use this for initialization
	void Start () {
        //
        for (int pN = 0; pN < 3; ++pN)
        {
            rotAngle[pN] = new Vector3(0, 120 * pN, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Input_Stick();
        Rotation_Selected();
        Selected();
	}

    //入力判定処理
    private void Input_Stick()
    {
        float input = Input.GetAxis("Horizontal");
        if (!stickDown && input != 0.0f)
        {
            stickDown = true;
            //右
            if (input > 0)
            {
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
        if (stickDown && !(input != 0))
        {
            stickDown = false;
        }
    }
    //回転
    private void Rotation_Selected()
    {
        Quaternion angle = Quaternion.Euler(rotAngle[PlayerNum()]);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotSpeed * Time.deltaTime);
    }
    //
    private int PlayerNum()
    {
        if(playerNum == Select_Players.Normal)
        {
            return 0;
        }
        else if(playerNum == Select_Players.Speed)
        {
            return 1;
        }
        return 2;
    }

    private void Selected()
    {
        if(Input.GetButtonDown("Fire1"))
        {
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
            Debug.Log("LoadScene");
        }
    }
}
