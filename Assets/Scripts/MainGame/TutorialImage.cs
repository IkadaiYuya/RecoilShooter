using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImage : MonoBehaviour
{
    [SerializeField] private GameObject tutorialImage_01 = null;
    [SerializeField] private GameObject tutorialImage_02 = null;

    Image image_01;
    Image image_02;

    private float alpha = 1.0f;

    [SerializeField] private float time;
    private float timeCnt;
    // Use this for initialization
    void Start()
    {
        timeCnt = 0;

        image_01 = tutorialImage_01.GetComponent<Image>();
        image_02 = tutorialImage_02.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCnt+=Time.deltaTime;
        if (timeCnt > time * 0.016f)
        {
            alpha -= 0.025f;
        }
        FadeOut();
    }
    void FadeOut()
    {
        image_01.color = new Color(1, 1, 1, alpha);
        image_02.color = new Color(1, 1, 1, alpha);
    }
}
