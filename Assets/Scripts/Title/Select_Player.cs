using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Player : MonoBehaviour {

    enum StickLR
    {
        Left = -1,
        Non = 0,
        Right = 1,
    }

    //機体の情報
    [SerializeField] private GameObject select_Center;
    //回転速度
    [SerializeField] private float rotSpeed = 360.0f;
    //長押し時に2フレーム以降か
    private bool stickDown = false;
    //
    private float inputHorizontal = 0.0f;
    //
    private StickLR stickLR = StickLR.Non;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (LStickDown())
        {
            if(stickLR == StickLR.Left)
            {
                select_Center.transform.rotation = Quaternion.Euler(0, -120, 0);
            }
        }
	}

    //アナログスティック入力判断
    //長押し時に1度だけtrueを返す
    private bool LStickDown()
    {
        //左右の入力値を代入
        inputHorizontal = Input.GetAxis("Horizontal");
        
        //入力されているか判断
        if (inputHorizontal != 0)
        {
            //入力値から左右を判断
            if(inputHorizontal > 0)
            {
                stickLR = StickLR.Right;
            }
            else
            {
                stickLR = StickLR.Left;
            }

            //前のフレームで入力されていなかったら
            if(stickDown == false)
            {
                stickDown = true;
                return true;
            }
        }
        //入力されていなかった
        else
        {
            stickLR = StickLR.Non;
            stickDown = false;
        }
        return false;
    }
}
