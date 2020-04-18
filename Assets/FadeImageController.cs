using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeImageController : MonoBehaviour
{
    [SerializeField]
    Image fadeImage;

    bool onceFlag;

    [SerializeField]
    float lerpValue;

    [SerializeField]
    bool isWin; // win : white 

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        if (isWin)
            fadeImage.color = new Color(255, 255, 255, 0);
        else
            fadeImage.color = new Color(0, 0, 0, 0);


        onceFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onceFlag)
        {
            StartCoroutine("fadeAnim");
            onceFlag = true;
        }
    }

    IEnumerator fadeAnim()
    {
        while (fadeImage.color.a < 1)
        {
            if (isWin)
            {
                timer += Time.deltaTime;

                if (timer > 3f)
                {
                    fadeImage.color += new Color(0, 0, 0, Mathf.Lerp(0, 1, Time.deltaTime * lerpValue));
                }
            }
            else
                fadeImage.color += new Color(0, 0, 0, Mathf.Lerp(0, 1, Time.deltaTime * lerpValue));

            yield return null;
        }

        SceneManager.LoadSceneAsync(0);
    }
}
