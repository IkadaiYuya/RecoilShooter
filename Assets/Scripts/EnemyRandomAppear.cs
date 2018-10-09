using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomAppear : MonoBehaviour {
    //敵の出現座標リスト
    [SerializeField] private List<Transform> EnemyPosition;
    //出現させる敵のリスト
    [SerializeField] private List<GameObject> Enemy;
    //出現タイマー
    private float timer = 0.0f;
    //出現間隔
    [SerializeField] private float time = 3;
    //第１フェーズ終了までの時間
    [SerializeField]private const float ferse1Time = 20;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RandomAppear();

    }
    private void RandomAppear()
    {
        timer += Time.deltaTime;
        if(timer <= ferse1Time)
        {
            if(timer % time <= 0.5f)
            {
                Instantiate(Enemy[Random.Range(0,10) % 2], EnemyPosition[Random.Range(0, EnemyPosition.Count)].position, transform.rotation);
            }
        }
        else
        {
            if(timer % time <= 0.5f)
            {
                Instantiate(Enemy[Random.Range(0, Enemy.Count)], EnemyPosition[Random.Range(0, EnemyPosition.Count)].position, transform.rotation);
            }
        }
    }
}
