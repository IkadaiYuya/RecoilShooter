using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(PlayerIsDead())
        {
            SceneManager.LoadScene("Results");
        }
	}

    private bool PlayerIsDead()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(player.Length);
        if(player.Length <= 0 )
        {
            return true;
        }
        return false;
    }
}
