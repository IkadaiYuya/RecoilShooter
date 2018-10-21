using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
	private Transform player;
	[SerializeField] private float distance = 10.0f;
	[SerializeField] private float speed = 0.2f;

    //
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
	{
        if(player != null)
        {
		    Vector3 targetPos = new Vector3(player.position.x, player.position.y + distance, player.position.z);
		    this.transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }
	}
}
