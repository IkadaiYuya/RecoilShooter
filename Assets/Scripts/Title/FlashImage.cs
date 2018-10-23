using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour {
    [SerializeField] private float step = 0.25f;

    Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        float toColor = image.color.a;

        if (Time.deltaTime != 0)
        {
            if (toColor < 0 || toColor > 1)
            {
                step = step * -1;
            }
        }
        image.color = new Color(255, 255, 255, toColor + step);
	}
}
