using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//仮のシーン変更処理

public class TempLoadTitle : MonoBehaviour {
    
    //
    

	// Use this for initialization
	void Start () {
        ScoreManager.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Push_Button");
            SceneManager.LoadScene("Title");
        }
	}
}
