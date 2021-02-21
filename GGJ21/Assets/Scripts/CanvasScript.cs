using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CanvasScript : MonoBehaviour
{
    public float fadeAlpha = 0.69f;
    public float fadeIncr;

    private Image fade;
    private Image image;
    private RectTransform rt;
    private Rect rect;
    private float DEFAULT_X = 100f;
    private float windowWidth = 1920f;
    private float windowHeight = 1080f;
    public static int memoryCycle = 0; //0 = inactive, 1 = fade in, 2 = wait, 3 = fade out
    
    
    private string nextScene;
    private bool deathFade = false;
    public GameObject pauseScreen=null;
    

    void Start()
    {
        fade = gameObject.GetComponent<Image>();
        image = gameObject.transform.Find("Image").GetComponent<Image>();
        rt = image.gameObject.GetComponent<RectTransform>();
        rect = rt.rect;

        DEFAULT_X = (float) (0 - image.preferredWidth / 2.0);
        //UnityEngine.Object pauseMenu = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/PauseScreen.prefab", typeof(GameObject));
        GameObject.Instantiate(pauseScreen, gameObject.transform);
    }

    public Text ScoreText;

    public void SetScoreText(int score)
    {
        ScoreText.text = "Score: " + score;
    }


    void Update()
    {
        if(memoryCycle != 0) memoryUpdate();
        if (deathFade) deathFadeUpdate();
    }

    public void showMemory(Sprite memory, string scene)
    {
        image.sprite = memory;
        nextScene = scene;
        memoryCycle = 1;
    }

    //Fading screen effect when the player dies
    public void playDeathFade()
    {
        PlayerMovement.active = false;
        deathFade = true;
    }

    void memoryUpdate()
    {
        //Update fading memory effect
        Color c;
        if (memoryCycle == 1)
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
                //update window width if the camera is available (sometimes it's null)
                windowWidth = Camera.current?.pixelRect.width ?? windowWidth;
                windowHeight = Camera.current?.pixelRect.height ?? windowHeight;

                float x = (float) (0 - image.preferredWidth/2.0);
                rt.anchoredPosition = new Vector2(x, windowHeight/2.0f+ (1 - c.a) * -image.preferredHeight);
            }
        }
        else if (memoryCycle == 2)
        {
            if (Input.anyKeyDown)
            {
                memoryCycle = 3;
            }
        }
        else if (memoryCycle == 3)
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

    void deathFadeUpdate()
    {
        Color c = fade.color;
        if (c.a >= 1f)
        {
            PlayerMovement.active = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            c.a += fadeIncr * Time.deltaTime;
            fade.color = c;
        }
    }
}
