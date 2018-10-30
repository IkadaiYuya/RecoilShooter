using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    //
    private static Canvas canvas;
    //
    private static Image image;
    //
    private static float alpha;
    //
    private static bool fadeInFlag = false;
    //
    private static bool fadeOutFlag = false;
    //
    [SerializeField] private static float fadeTime = 0.5f;
    //
    private static GameObject CanvasPos;

    static void Init()
    {
        GameObject canvastemp = new GameObject("CanvasFade");
        canvas = canvastemp.AddComponent<Canvas>();
        canvastemp.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvastemp.AddComponent<FadeManager>();

        //
        canvas.sortingOrder = 100;

        image = new GameObject("imageFade").AddComponent<Image>();
        image.transform.SetParent(canvas.transform, false);
        image.rectTransform.anchoredPosition = Vector3.zero;
        //
        image.rectTransform.sizeDelta = new Vector2(1920, 1080);
    }

    public static void FadeIn()
    {
        if (image == null)
            Init();
        fadeInFlag = true;
    }

    public static void FadeOut()
    {
        if (image == null)
            Init();
        canvas.enabled = true;
        fadeOutFlag = true;
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
                canvas.enabled = false;
            }

            image.color = new Color(0, 0, 0, alpha);
        }
        else if(fadeOutFlag)
        {
            alpha += Time.deltaTime / fadeTime;

            //
            if(alpha >= 1)
            {
                fadeOutFlag = false;
                alpha = 1.0f;
            }

            //
            image.color = new Color(0, 0, 0, alpha);
        }
	}
}
