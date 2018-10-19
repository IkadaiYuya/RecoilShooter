using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtForword : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(new Vector3(500, 0, -500));	
	}
}
