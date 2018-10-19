using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayer : MonoBehaviour {

    //
    [SerializeField] private List<GameObject> player;

	// Use this for initialization
	void Start () {
        //
        Instantiate(player[Select_Player.selected_Player], transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
