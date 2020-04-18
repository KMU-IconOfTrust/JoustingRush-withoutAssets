using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointController : MonoBehaviour
{
    // 트리거의 Enter Exit을 통해 옳은 공격인지 확인한 후 결과를 전달

    int process;

    [SerializeField]
    public bool isMainMenu;
    [SerializeField]
    public int stage;
    [SerializeField]
    public bool isDashStart;
    [SerializeField]
    public bool reBattle;

    PlayerController PC; // 인게임씬

    [SerializeField]
    StageSelector SS; // 메뉴씬

    private void Awake()
    {
        process = 0;
        
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {

    }

    // true: cut
    // false: stab
    public void checkCollectHit(bool isCutAct)
    {
        if (!isMainMenu)
        {
            switch (process)
            {
                case 0:
                    if (isCutAct)
                    {
                        process++;
                    }
                    else
                    {
                        // 오동작
                        returnResult(false);
                    }
                    break;
                case 1:
                    if (!isCutAct)
                    {
                        // 성공
                        returnResult(true);
                    }
                    else
                    {

                        returnResult(false);

                    }
                    break;
            }
        }
    }

    // 동작의 결과
    public void returnResult(bool result)
    {
        if (reBattle)
        {
            GameObject.Find("GameManager").GetComponent<GameController>().HitPointBtnFlag = true;
        }
        else if (isDashStart) {
            GameObject.Find("GameManager").GetComponent<GameController>().HitPointBtnFlag = true;
        }

        // 인게임
        else if (!isMainMenu)
        {
            // 1:true , 0:false
            PC.attack = result ? 1 : 0;
        }

        //Destroy 어디있지
        //PC에서 맞는지 틀린지 확인하고 Destroy
        
        // 메인메뉴(스테이지 선택)
        else
        {
            SS.stageSelector(stage);
        }
    }
}
