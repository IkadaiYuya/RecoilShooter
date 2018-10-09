using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPosition : MonoBehaviour {
    public Color _color = Color.yellow;
    public float _redius = 0.1f;
    // Use this for initialization

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _redius);
    }
}
