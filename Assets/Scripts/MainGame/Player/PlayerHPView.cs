using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPView : MonoBehaviour
{

    //HPのUI
    [SerializeField] private GameObject[] hpView = new GameObject[3];
    //UIの色などの指定用
    [SerializeField] private Image[] hpImage = new Image[3];
    //ダメージ時にHPのUIを表示する時間
    [SerializeField] private float viewTime = 2.0f;
    //HPのUI用タイマー
    private float viewTimer = 0;
    //点滅の秒
    [SerializeField] private float onoff = 0.4f;
    //フェードアウトする時間
    [SerializeField] private float fadeOutTime = 0.1f;
    //フェードアウト用タイマー
    private float fadeOutTimer = 0;

    private int cnt = 0;


    // Use this for initialization
    void Start()
    {
        foreach (GameObject v in hpView)
        {
            v.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //HPのUI表示関数
    //表示が終わったらfalseを返す
    //引数:(体力)
    public bool HPView(int hp)
    {
        HideHP();
        viewTimer += Time.deltaTime;
        if(hp > 1)
        {
            return HPViews(hp);
        }
        else if(hp == 1)
        {
            LastHPView();
            return true;
        }
        else
        {
            return false;
        }
    }

    //ダメージを受けた後のUI表示
    //表示を消したらfalseを返す
    private bool HPViews(int hp)
    {
        if(viewTime + fadeOutTime - fadeOutTimer > 0)
        {
            if(viewTime - fadeOutTimer > 0)
            {
                for (int i = 0; i < hp;++i)
                {
                    hpView[i].SetActive(true);
                }
            }
            else
            {
                for(int i = 0; i < hp;++i)
                {
                    float alpha = 1 - (fadeOutTimer - viewTime) / fadeOutTime;
                    Debug.Log("FT:" + fadeOutTimer);
                    Debug.Log("AL:" + alpha);
                    Debug.Log("AL-:" + (fadeOutTimer - viewTime) / fadeOutTime);
                    Debug.Log("FONum:" + cnt);
                    cnt++;
                    hpImage[i].color = new Color(1, 1, 1, alpha);
                }
            }
        }
        else
        {
            HideHP();
            return false;
        }
        fadeOutTimer += Time.deltaTime;
        return true;
    }

    //残りHPが1の時のUI表示
    private void LastHPView()
    {
        hpView[0].SetActive(true);
        float alpha = Mathf.Cos(viewTimer / onoff * 60.0f * Mathf.PI / 180.0f);
        hpImage[0].color = new Color(0.9f, 0.5f, 0.5f, alpha);
    }

    //
    private void HideHP()
    {
        foreach(GameObject v in hpView)
        {
            v.SetActive(false);
        }
    }
}