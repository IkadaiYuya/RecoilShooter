using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPView : MonoBehaviour {

    //HPのUI
    [SerializeField] private GameObject[] hpView = new GameObject[3];
    //ダメージ時にHPのUIを表示する時間
    [SerializeField] private float viewTime = 2.0f;
    //HPのUI用タイマー
    private float viewTimer = 0;
    //
    private float lastHPTimer = 0;
    //
    [SerializeField] private Image[] hpImage = new Image[3];
    //点滅の秒
    [SerializeField] private float onoff = 0.4f;
    //
    [SerializeField] private float fadeOutTime = 0.1f;
    //
    private float fadeOutTimer = 0;


    // Use this for initialization
    void Start () {
        foreach(GameObject v in hpView)
        {
            v.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    //HPのUI表示関数
    //表示が終わったらfalseを返す
    //引数:(ダメージを受けたか,体力)
    public bool HPView(int hp)
    {
        if(viewTime - viewTimer > 0)
        {
            for(int i = 0; i < hp;++i)
            {
                hpView[i].SetActive(true);
            }
        }
        else
        {
            viewTimer = 0;
            return HPViewFadeOut();
        }
        viewTimer += Time.deltaTime;
        return true;
    }

    public void LastHPView()
    {
        //一旦すべてを非表示に
        foreach(GameObject v in hpView)
        {
            v.SetActive(false);
        }
        //最後のHPのUIを表示
        hpView[0].SetActive(true);
        //点滅させる
        hpImage[0].color = new Color(0.5f, 0.5f, 0.5f, Mathf.Cos(lastHPTimer * 3.161592f / 180.0f));
        lastHPTimer += Time.deltaTime * 360.0f / onoff;
    }

    public void LostHPView()
    {
        foreach(GameObject v in hpView)
        {
            v.SetActive(false);
        }
    }

    private bool HPViewFadeOut()
    {
        foreach(Image h in hpImage)
        {
            float alpha = Mathf.Cos(fadeOutTimer * 3.141592f / 180.0f);
            Debug.Log(alpha);
            if(alpha < 0)
            {
                alpha = 0;
                foreach(GameObject v in hpView)
                {
                    v.SetActive(false);
                }
                return false;
            }
            h.color = new Color(1, 1, 1, alpha);
        }
        fadeOutTimer += Time.deltaTime * 90.0f * 60.0f / fadeOutTime;
        return true;
    }
}
