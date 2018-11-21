using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour {

    //接触判定
    private void OnTriggerEnter(Collider other)
    {
        //接触したコライダーのタグが移動限界値だったら
        if(other.gameObject.tag == "LimitRenge")
        {
            //消滅させる
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
