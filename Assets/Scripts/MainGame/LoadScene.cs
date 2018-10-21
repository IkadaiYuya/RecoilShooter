using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    //プレイヤー消滅後待機時間
    [SerializeField] private float waitTime = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(PlayerIsDead())
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject ene in enemys)
            {
                Destroy(ene);
            }
            SceneManager.LoadScene("Results");
        }
	}

    private bool PlayerIsDead()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if(player.Length <= 0 )
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0)
            {
                return true;
            }
        }
        return false;
    }
}
