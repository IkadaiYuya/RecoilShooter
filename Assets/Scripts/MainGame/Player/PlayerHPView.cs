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
    //フェードアウトする時間
    [SerializeField] private float fadeOutTime = 0.1f;
    //HPのUI表示とフェードアウト用タイマー
    private float hpViewTimer = 0;
    //残りHPが1の時の点滅間隔
    [SerializeField] private float flashing = 0.4f;
     //HPのUI用タイマー
    private float flashingTimer = 0;


    // Use this for initialization
    void Start()
    {
        //すべてのHPのUIを非表示に
        HideHP();
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
        //一旦すべてを非表示に
        HideHP();
        //残りHPが1より多いなら
        if(hp > 1)
        {
            //フェードアウトまで終わったらfalseを返してくる
            return HPViews(hp);
        }
        //１なら
        else if(hp == 1)
        {
            LastHPView();
            //ずっと表示させる
            return true;
        }
        //基本は入らない
        else
        {
            //もし入ってきても表示しない
            return false;
        }
    }

    //ダメージを受けた後のUI表示
    //表示を消したらfalseを返す
    private bool HPViews(int hp)
    {
        //経過時間がフェードアウトまでを含めた表示時間を超えていないなら
        if(viewTime + fadeOutTime - hpViewTimer > 0)
        {
            //表示時間内なら
            if(viewTime - hpViewTimer > 0)
            {
                //残りHP分表示
                for (int i = 0; i < hp;++i)
                {
                    hpView[i].SetActive(true);
                }
            }
            //フェードアウトのタイミングなら
            else if(fadeOutTime - (hpViewTimer - viewTime) > 0)
            {
                //残りHPをフェードアウトさせる
                for(int i = 0; i < hp;++i)
                {
                    //指定したHPを表示
                    hpView[i].SetActive(true);
                    //不透明度をコサインを使って指定
                    //フェードアウトする時間いっぱい使う
                    float alpha = Mathf.Cos((hpViewTimer - viewTime) / fadeOutTime * 60.0f * Mathf.PI / 180.0f);
                    //上で計算した不透明度を指定したHPのUIにあてる
                    hpImage[i].color = new Color(1, 1, 1, alpha);
                }
            }
        }
        //フェードアウトまで終了したら
        else
        {
            //すべてを非表示に
            HideHP();
            //HP表示終了
            return false;
        }
        //タイマー加算
        hpViewTimer += Time.deltaTime;
        //まだ表示し続ける
        return true;
    }

    //残りHPが1の時のUI表示
    private void LastHPView()
    {
        //最後のHPのUIだけ表示
        hpView[0].SetActive(true);
        //不透明度をコサインを使って指定
        //設定した間隔で点滅させる
        float alpha = Mathf.Cos(flashingTimer / flashing * 60.0f * Mathf.PI / 180.0f);
        //最後のHPに計算した不透明を指定
        hpImage[0].color = new Color(0.9f, 0.5f, 0.5f, alpha);
        //タイマーを加算
        flashingTimer += Time.deltaTime;
    }

    //HPを非表示にする
    public void HideHP()
    {
        foreach(GameObject v in hpView)
        {
            v.SetActive(false);
        }
    }
}