using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    /************************
     * 
     *  플레이어에게 적용되는 배틀 컨트롤러 (싱글 모드 기준)
     * 
     *  1) 적이 근처에 왔음을 확인한다
     *  2) 전투가 시작된다
     *  3) 전투가 진행된다
     *  4) 전투가 종료된다
     *  
     * **********************/


    GameController GC;
    BGMController BGMC;
    DashTester dashTester_player;
    DashTester dashTester_enemy;
    int controlSpeed; // 감속/가속 할 수치

    public bool isBattleEnd;

    private void Awake()
    {
        
        controlSpeed = 100; // 10
        dashTester_player = transform.parent.GetComponent<DashTester>();
        dashTester_enemy = GameObject.Find("Enemy").GetComponent<DashTester>();

        GC = GameObject.Find("GameManager").GetComponent<GameController>();
        BGMC = GameObject.Find("SoundManager").GetComponent<BGMController>();
    }
    private void Start()
    {
        isBattleEnd = false;
    }

    private void Update()
    {
        if (GC.STATE == 5)
        {
            BattleEnd();
            GC.STATE = 6;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // 자동 배틀 시작
        if (other.CompareTag("Enemy"))
        {
            BGMC.bgmOn();
            //dashTester_player.speed *= 1f / controlSpeed; // 플레이어 10배 감속
            dashTester_player.DashSlower(controlSpeed);
            //other.transform.parent.GetComponent<DashTester>().speed *= 1f / controlSpeed; // 적 10배 감속
            other.transform.parent.GetComponent<DashTester>().DashSlower(controlSpeed);


            GC.STATE = 2;
        }

  
    }
    
    // 배틀 종료시, 양 측 속도를 원상복귀
    public void BattleEnd()
    {
        BGMC.bgmOff();
        //dashTester_player.speed *= controlSpeed; // 플레이어 가속(원래 속도로)
        dashTester_player.DashFaster();
        //dashTester_enemy.speed *= controlSpeed; // 적 가속(원래 속도로)
        dashTester_enemy.DashFaster();
    }

    
    private void OnTriggerExit(Collider other)
    {
        //dashTester.speed *= controlSpeed; // 플레이어 가속(원래 속도로)
        //other.transform.parent.GetComponent<DashTester>().speed *= controlSpeed; // 적 가속(원래 속도로)
        // 자동 배틀 시작
        if (other.CompareTag("Enemy"))
        {
            BattleEnd();
        }
   
    }
    
}
