﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    public float fadeAlpha = 0.69f;
    public float fadeIncr;

    private Image fade;
    private Image image;
    private RectTransform rt;
    private Rect rect;
    private readonly float DEFAULT_X = 100f;
    public static int memoryCycle = 0; //0 = inactive, 1 = fade in, 2 = wait, 3 = fade out
    private string nextScene;

    void Start()
    {
        fade = gameObject.GetComponent<Image>();
        image = gameObject.transform.Find("Image").GetComponent<Image>();
        rt = image.gameObject.GetComponent<RectTransform>();
        rect = rt.rect;
    }

    void Update()
    {
        //Update fading memory effect
        Color c;
        if(memoryCycle == 1)
        {
            if ((string.Equals(nextScene, string.Empty) && fade.color.a < fadeAlpha) || (!string.Equals(nextScene, string.Empty) && fade.color.a < 1f))
            {
                c = fade.color;
                c.a += fadeIncr * Time.deltaTime;
                if (string.Equals(nextScene, string.Empty) && c.a >= fadeAlpha) c.a = fadeAlpha;
                if (!string.Equals(nextScene, string.Empty) && c.a >= 1f) c.a = 1f;
                fade.color = c;
            }
            else
            {
                c = image.color;
                c.a += fadeIncr * Time.deltaTime;
                if (c.a >= 1f)
                {
                    c.a = 1f;
                    memoryCycle = 2;
                }
                image.color = c;
                rt.anchoredPosition = new Vector2(DEFAULT_X, (1 - c.a) * -100);
            }
        }
        else if(memoryCycle == 2)
        {
            if (Input.anyKeyDown)
            {
                memoryCycle = 3;
            }
        }
        else if(memoryCycle == 3)
        {
            if (image.color.a > 0f)
            {
                c = image.color;
                c.a -= fadeIncr * Time.deltaTime;
                if (c.a <= 0f) c.a = 0f;
                image.color = c;
            }
            else
            {
                if (!string.Equals(nextScene, string.Empty))
                {
                    memoryCycle = 0;
                    PlayerMovement.active = true;
                    SceneManager.LoadScene(nextScene);
                }
                else
                {
                    c = fade.color;
                    c.a -= fadeIncr * Time.deltaTime;
                    if (c.a <= 0f)
                    {
                        c.a = 0f;
                        memoryCycle = 0;
                        PlayerMovement.active = true;
                    }
                    fade.color = c;
                }
            }
        }
    }

    public void showMemory(Sprite memory, string scene)
    {
        image.sprite = memory;
        nextScene = scene;
        memoryCycle = 1;
    }
}
