using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadSceneAnimController : MonoBehaviour
{
    [SerializeField]
    Animator horse;

    [SerializeField]
    Animator rider;

    [SerializeField]
    Camera camera;

    [SerializeField]
    GameObject[] Stage;

    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        horse.speed = 0.05f;
        rider.speed = 0.3f;

        if(StageController.selectStage == 1)
        {
            Stage[0].SetActive(true);
            Stage[1].SetActive(false);
        }
        else if(StageController.selectStage == 2)
        {
            Stage[0].SetActive(false);
            Stage[1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10f)
        {
            SceneManager.LoadSceneAsync(0);
        }

        camera.transform.Translate(new Vector3(0, 0, -Time.deltaTime));
    }
}
