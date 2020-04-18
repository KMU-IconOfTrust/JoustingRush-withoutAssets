using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineChecker : MonoBehaviour
{
    // 전투가 끝나고 길의 끝까지 왔을 경우
    // 재시작을 위한 스크립트.
    // 양측 속도를 0으로 만든다
    // 게임 종료에 따른 진행 방향을 결정한다(재시작, 다음라운드 등)
    // 키 입력 대기 후 진행한다


    GameController GC;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.Find("GameManager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            

            // 정지 및 대전 종료.
            GC.STATE = 7;
        }
    }
}
