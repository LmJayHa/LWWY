using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutBlack : MonoBehaviour
{

    public static FadeInOutBlack fadeInOutBlack
    {
        get
        {
            if (f_instance == null)
            {
                f_instance = FindObjectOfType<FadeInOutBlack>();
            }
            return f_instance;
        }
    }

    private static FadeInOutBlack f_instance;

    public Image BlackUI;

    private bool isFadeIn;

    private bool isFadeOut;

    private float startTime;
    private float addingTime;

    float r;
    float g;
    float b;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isFadeIn)
        {
            FadeIn();

        }
        else if (isFadeOut) {
            FadeOut();
        }

    }



    private void FadeIn() {
        if (startTime + addingTime > Time.time)
        {
            float Setting = BlackUI.color.a + Time.deltaTime / addingTime;
            BlackUI.color = new Color(r, g, b, Setting);
        }
        else {
            BlackUI.color = new Color(r, g, b, 1);
            isFadeIn = false;
        }
    }

    private void FadeOut()
    {
        if (startTime + addingTime > Time.time)
        {
            float Setting = BlackUI.color.a - Time.deltaTime / addingTime;
            BlackUI.color = new Color(r, g, b, Setting);
        }
        else
        {
            BlackUI.color = new Color(r, g, b, 0);
            BlackUI.gameObject.SetActive(false);
            isFadeOut = false;
        }
    }

    public void SetFadeIn(float adding) {
        r = BlackUI.color.r;
        g = BlackUI.color.g;
        b = BlackUI.color.b;
        BlackUI.gameObject.SetActive(true);
        BlackUI.color = new Color(r, g, b, 0);
        addingTime = adding;
        startTime = Time.time;
        isFadeIn = true;
    }

    public void SetFadeOut(float adding) {
        r = BlackUI.color.r;
        g = BlackUI.color.g;
        b = BlackUI.color.b;
        BlackUI.color = new Color(r, g, b, 1);
        addingTime = adding;
        startTime = Time.time;
        isFadeOut = true;
    }

    public void SetColor(int idx) {
        switch (idx) {
            case 1:
                BlackUI.color = new Color(221f / 255f, 218f / 255f, 210f / 255f, 225f / 255f);
                break;
            case 2:
                BlackUI.color = new Color(0f / 225f, 0f / 255f, 0f / 255f, 255f / 255f);
                break;
            case 3:
                BlackUI.color = new Color(37f / 255f, 37f / 255f, 37f / 255f, 225f / 255f);
                break;
            case 4:
                BlackUI.color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 225f / 255f);
                break;

        }

    }//1낮,2밤,3노을




}
