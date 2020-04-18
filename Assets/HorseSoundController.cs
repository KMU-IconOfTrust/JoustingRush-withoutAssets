using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseSoundController : MonoBehaviour
{
    [SerializeField]
    AudioSource[] runSounds;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;

        Debug.Log("[0] 재생");
        runSounds[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 1f)
        {

            
            if (timer > 0.2f)
            {
                if (!runSounds[2].isPlaying)
                {
                    Debug.Log("[2] 재생");
                    runSounds[2].Play();
                }
            }
            else if (timer > 0.1f)
            {
                if (!runSounds[1].isPlaying)
                {
                    Debug.Log("[1] 재생");
                    runSounds[1].Play();
                }
            }
        }
    }
}
