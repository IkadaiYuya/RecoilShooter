using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjectRotate : MonoBehaviour {

    //回転速度
    [SerializeField] private float rotSpeed = 60.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //指定した速さででY軸回転させる
        transform.Rotate(new Vector3(0, -rotSpeed, 0) * Time.deltaTime, Space.Self);
	}
}
