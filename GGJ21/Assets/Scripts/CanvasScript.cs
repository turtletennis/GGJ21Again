using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public float fadeAlpha = 0.69f;
    public float fadeIncr;

    private Image fade;
    private Image image;
    private int memoryCycle = 0; //0 = inactive, 1 = fade in, 2 = wait, 3 = fade out

    void Start()
    {
        fade = gameObject.GetComponent<Image>();
        image = gameObject.transform.Find("Image").GetComponent<Image>();
    }

    void Update()
    {
        Color c;
        if(memoryCycle == 1)
        {
            if (fade.color.a < fadeAlpha)
            {
                c = fade.color;
                c.a += fadeIncr * Time.deltaTime;
                if (c.a >= fadeAlpha) c.a = fadeAlpha;
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
                c = fade.color;
                c.a -= fadeIncr * Time.deltaTime;
                if (c.a <= 0f)
                {
                    c.a = 0f;
                    memoryCycle = 0;
                }
                fade.color = c;
            }
        }
    }

    public void showMemory(Sprite memory)
    {
        image.sprite = memory;
        memoryCycle = 1;
    }
}
