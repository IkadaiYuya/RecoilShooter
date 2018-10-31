using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    //
    private Image image;
    //
    private float alpha;
    //
    private bool fadeInFlag = false;
    //
    private bool fadeOutFlag = false;
    //
    [SerializeField] private float fadeTime = 0.5f;
    //
    private string nextScene = "";

    void Start()
    {
        image = GameObject.Find("FadeImage").GetComponent<Image>();
    }

    public void FadeIn()
    {
        if (!fadeInFlag && !fadeOutFlag)
        {
            Debug.Log("FadeIn");
            fadeInFlag = true;
            alpha = 1.0f;
        }
    }

    public void FadeOut(string nextS)
    {
        if (!fadeInFlag && !fadeOutFlag)
        {
            nextScene = nextS;
            fadeOutFlag = true;
            alpha = 0.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(fadeInFlag)
        {
            alpha -= Time.deltaTime / fadeTime;

            //
            if(alpha <= 0)
            {
                fadeInFlag = false;
                alpha = 0.0f;
            }

            image.color = new Color(0, 0, 0, alpha);
            Debug.Log(image.color);
        }
        else if(fadeOutFlag)
        {
            alpha += Time.deltaTime / fadeTime;

            //
            if(alpha >= 1)
            {
                fadeOutFlag = false;
                alpha = 1.0f;
                if(nextScene == "Title" || nextScene == "MainGame")
                {
                    ScoreManager.score = 0;
                }
                //次のシーン呼び出し
                SceneManager.LoadScene(nextScene);
            }

            //
            image.color = new Color(0, 0, 0, alpha);
        }
    }
}
