using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.Play();
            StageController.selectStage = 1;
            //SceneManager.LoadScene(2);
            SceneManager.LoadSceneAsync(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource.Play();
            StageController.selectStage = 2;
            //SceneManager.LoadScene(2);
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void stageSelector(int stage)
    {
        if(stage == 1)
        {
            audioSource.Play();
            StageController.selectStage = 1;
            //SceneManager.LoadScene(2);
            SceneManager.LoadSceneAsync(1);
        }
        else if (stage == 2)
        {
            audioSource.Play();
            StageController.selectStage = 2;
            //SceneManager.LoadScene(2);
            SceneManager.LoadSceneAsync(1);
        }
    }
}
