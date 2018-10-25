using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomAppear : MonoBehaviour {
    //敵の出現座標リスト
    [SerializeField] private List<Transform> WavesPosition;
    //出現させる隊の敵のリスト
    [SerializeField] private List<GameObject> Waves;
    //出現タイマー
    private float timer = 0.0f;
    //出現間隔
    [SerializeField] private float time = 3;
    //第１フェーズ終了までの時間
    [SerializeField] private const float ferse1Time = 20;
    //隊列生成最大数の増加時間間隔
    [SerializeField] private float maxWavesUpTime = 30;
    //現在の敵の隊列の数
    private int wavesNum = 0;
    //敵の最大数
    [SerializeField] private int maxWavesNum = 1;
    //
    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //突撃部隊(弱)をランダムな場所に生成
        Instantiate(Waves[1], WavesPosition[Random.Range(0, WavesPosition.Count)].position, transform.rotation);
    }

    // Update is called once per frame
    void Update () {
        //プレイヤーが生きていたら
        if(player != null)
        {
            //現在のプレイ時間から生成する敵の隊列の数の最大数を判断する
            EnemysWaveNowMax();
            //現在出現してる敵の数を数える
            FindEnemysWaveNum();
            //生成されている数が最大数に達していないなら生成
            if(wavesNum < maxWavesNum )
            {
                RandomAppear();
            }
            //タイマー加算
            timer += Time.deltaTime;
        }
    }

    //生成処理
    private void RandomAppear()
    {
        //第1フェーズか判断
        if (timer <= ferse1Time)
        {//第１フェーズなら
            //時間になったら
            if(time - timer <= 0)
            {
                //第１フェーズでは体力の少ない3部隊から生成
                Instantiate(Waves[Random.Range(0, 10) % 3], WavesPosition[Random.Range(0, WavesPosition.Count)].position, transform.rotation);
                //タイマー初期化
                timer = 0;
            }
        }
        else
        {//第１フェーズが終了したら
            //時間になったら
            if(time - timer <= 0)
            {
                //第2フェーズではすべての隊列から生成
                Instantiate(Waves[Random.Range(0, Waves.Count)], WavesPosition[Random.Range(0, WavesPosition.Count)].position, transform.rotation);
                //タイマー初期化
                timer = 0;
            }
        }
    }

    //現在のプレイ時間から生成する敵の隊列の数の最大数を判断する
    private void EnemysWaveNowMax()
    {
        //第１フェーズなら
        if(Time.time <= ferse1Time)
        {
            //何もせず終了
            return;
        }
        //第１フェーズ以降で一定時間経つと
        else if ((Time.time - ferse1Time) % maxWavesUpTime < 0.016f)
        {
            //敵部隊の最大生成数の増加
            maxWavesNum++;
        }
    }

    //現在出現してる敵の数を数える
    private void FindEnemysWaveNum()
    {
        //配列にタグがEnemyWaveのものを保存
        GameObject[] WavesNum_ = GameObject.FindGameObjectsWithTag("EnemysWave");
        //現在の敵部隊の数を配列の長さから算出
        wavesNum = WavesNum_.Length;
    }
}
