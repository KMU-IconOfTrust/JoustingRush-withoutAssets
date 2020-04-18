using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedCameraEffect : MonoBehaviour
{
    [SerializeField]
    Image effectImage;

    bool toRed;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        toRed = false;
    }
    
    
    public void Damaged()
    {
        toRed = true;
        StartCoroutine("_Damaged");
    }

    IEnumerator _Damaged()
    {
        while (true)
        {
            // 빨갛게
            if (toRed)
            {

                effectImage.color += new Color(0f, 0f, 0f, Time.deltaTime);
                if(effectImage.color.a > 0.7)
                {
                    toRed = false;
                }
            }
            // 되돌리기
            else
            {
                effectImage.color -= new Color(0f, 0f, 0f, Time.deltaTime);
                if(effectImage.color.a <= 0f)
                {
                    StopCoroutine("_Damaged");
                }
            }

            yield return null;
        }
    }
}
