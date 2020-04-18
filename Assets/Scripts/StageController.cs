using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    // 스테이지 설정( Renderer.enabled = true/false )
    // 

    // 메인메뉴에서 선택한 스테이지
    public static int selectStage;
    // 스테이지 오브젝트 0 = 이상한 맵 // 1 = 콜로세움
    [SerializeField]
    List<GameObject> Stage = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        switch (selectStage)
        {
            case 1:
                Stage[1].SetActive(false);
                break;
            case 2:
                Stage[0].SetActive(false);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
