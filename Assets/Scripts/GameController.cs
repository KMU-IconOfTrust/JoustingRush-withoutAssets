using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    PlayerController PC;
    DashTester P_DT;

    [SerializeField]
    DamagedCameraEffect DCE;

    EnemyController EC;
    DashTester E_DT;

    [SerializeField]
    GameObject Btn;
    [SerializeField]
    GameObject BtnCase; // 버튼이 담기는 곳 (케이스)
    GameObject generatedBtn;
    public bool HitPointBtnFlag;
    bool isBtnGenerated;

    // 상태. PlayerController, EnemyController 는 이 STATE를 참조해 동작
    // switch를 순환하며 상태 기록
    // 0: 대기
    // 1: 출발
    // 2: 전투 시작 (감속)
    // <-- 전투 중 -->
    // 3: 적공격 & 플레이어방어
    // 4: 적방어 & 플레이어공격
    // 5: 전투 종료 (가속)
    // 6: 달리는 중 (스킵 키 입력 대기)
    // 7: 대전 종료 (정지 및 키 입력 대기)
    public int STATE;

    // 공격을 주고받는 최대 횟수
    int BATTLE_COUNT;

    // 플레이어의 방어, 공격 이 끝나기를 확인하는 변수
    // 이 신호가 변경되거나 시간이 오버되거나 둘 중 하나를
    // 기다리가 된다?
    public bool waitPlayerAct;

    private void Awake()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        EC = GameObject.Find("Enemy").GetComponent<EnemyController>();

        P_DT = GameObject.Find("Player").GetComponentInChildren<DashTester>();
        E_DT = GameObject.Find("Enemy").GetComponentInChildren<DashTester>();

        HitPointBtnFlag = false;
        isBtnGenerated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        waitPlayerAct = false;
        BATTLE_COUNT = 3;
        STATE = 0;
    }

    // Update is called once per frame
    void Update()
    {


        switch (STATE)
        {
            case 0:
                //양측 준비 및 대기
                BattleReady(); // 위치로

                // 키 입력 후 전투시작
                if (Input.GetKeyDown(KeyCode.Space) || HitPointBtnFlag)
                {
                    STATE = 1;

                    HitPointBtnFlag = false;
                    isBtnGenerated = false;
                    Destroy(generatedBtn);

                    PC.state = 1;
                    EC.state = 1;
                }
                break;
            case 1:
                //Debug.Log("PROCESS STATE >> " + STATE);
                // 달리는 중
                // BattleController가 전투가 시작되면(트리거가 맞닿으면)
                // 감속 후 STATE를 2로 설정해 줌
                break;
            case 2:
                // 감속 된 상태.
                PC.state = 3;
                EC.state = 3;

                // 플레이어의 방어 기다림
                waitPlayerAct = true;
                STATE = 3;

                break;
            case 3:
                // 플레이어에게 공격 기회가 하나 남아있었다면
                // STATE를 바로 4로 다시 넘겨줌?
                if (EC.chance == 1)
                {
                    Debug.Log("두 번째 공격");
                    PC.attackPoint = -1;// 초기화
                    PC.guardPoint = -1;
                    PC.state = 4;
                    EC.state = 4;

                    EC.isOnAct = true;
                    // 플레이어의 공격 기다림
                    waitPlayerAct = true;
                    STATE = 4;

                }
                else
                {
                    // 적공격 & 플레이어방어
                    if (waitPlayerAct) // 플레이어의 동작을 기다리는중~~~
                    {
                        return;
                    }

                    if (EC.attackPoint == PC.guardPoint)
                    {
                        Debug.Log("방어 성공");
                        PC.soundPlayer(1);
                    }
                    else
                    {
                        Debug.Log("방어 실패");
                        PC.soundPlayer(0);
                        DCE.Damaged();
                        PC.HP--;
                    }

                    // 내 HP가 다 까였다면
                    if (PC.HP == 0)
                    {
                        Debug.Log("패배!");

                        // 죽는 애니메이션 보여주고 메인화면으로 날려버리기?
                        // 연출. 캐릭터 렌더링 안해도 되게?
                        // 화면 빨갛게, 하늘 바라보게, 페이드 아웃, 씬 넘기기

                        SceneManager.LoadScene(3);

                    }
                    else
                    {
                        // 플레이어에게 공격권이 넘어옴

                        PC.guardPoint = -1; // 초기화
                        PC.state = 4;
                        EC.state = 4;
                        EC.chance = 2; // 두 번의 기회

                        // 플레이어의 공격 기다림
                        waitPlayerAct = true;
                        STATE = 4;
                    }
                }
                break;
            case 4:
                // 적방어 & 플레이어공격
                if (waitPlayerAct) // 플레이어의 동작을 기다리는중~~~
                {
                    return;
                }
                
                // Enemy의 guardPoint는 player가 공격해야 할 위치.
                // 정확히 제때 공격하지 못하면 공격 실패(Enemy 방어 성공)

                Debug.Log("attackPoint >> " + PC.attackPoint);
                Debug.Log("guardPoint >> " + EC.guardPoint);

                // vr로 HitPoint를 정상 파괴했거나
                // 키보드로 정상 입력한 경우
                if(PC.attack == 1 || PC.attackPoint == EC.guardPoint)
                {
                    Debug.Log("공격 성공");
                    EC.animator.SetTrigger("IsDamage");
                    PC.soundPlayer(0);
                    EC.chance--;
                    EC.HP--;
                }
                else
                {
                    Debug.Log("공격 실패");
                    EC.animator.SetTrigger("BlockShield");
                    PC.soundPlayer(1);
                    EC.chance = 0;
                }

                if (EC.chance == 0)
                {
                    BATTLE_COUNT--;
                }

                // 전투 횟수가 남았다면
                if (EC.HP > 0 && BATTLE_COUNT > 0) 
                {
                    PC.attack = -1;
                    PC.attackPoint = -1;// 초기화
                    EC.guardPoint = -1;

                    PC.state = 3;
                    EC.state = 3;
                    waitPlayerAct = true;
                    EC.isOnAct = false;
                    STATE = 3;
                    
                }

                // 적의 HP가 다 깎였다면
                else if(EC.HP == 0)
                {
                    Debug.Log("승리!");

                    // 사망 애니메이션 진행
                    // 연출
                    // 적 낙마 후 쓰러지는 애니메이션 슬로우?
                    // 승패 여부 결과 출력 가능?
                    // 애니메이션 종료 후 씬 넘이기

                    SceneManager.LoadScene(2);

                }

                //누구도 죽지 않고 한 차례의 경합이 끝났다면
                else 
                {
                    PC.attack = -1;
                    PC.attackPoint = -1;
                    EC.guardPoint = -1;
                    PC.state = 5;
                    EC.state = 5;
                    STATE = 5;
                }
                break;

            case 5:
                // 전투종료. 가속
                //BC.BattleEnd();

            

                break;
            case 6:
                if (!isBtnGenerated)
                {
                    isBtnGenerated = true;
                    generatedBtn = Instantiate(Btn, BtnCase.transform.position, Quaternion.identity, BtnCase.transform);
                    generatedBtn.GetComponent<HitPointController>().reBattle = true;
                }
                // 가속된 후 달리는 중.
                // 스킵 입력 대기
                if (Input.GetKeyDown(KeyCode.Space) || HitPointBtnFlag)
                {
                    STATE = 0;
                    HitPointBtnFlag = false;
                    isBtnGenerated = false;
                    Destroy(generatedBtn);
                }
                break;
            case 7:
                // 정지. 대전 종료
                // HP 확인 후 진행 여부 판단
                P_DT.DashStop();
                E_DT.DashStop();

                if (Input.GetKeyDown(KeyCode.Space) || HitPointBtnFlag)
                {
                    HitPointBtnFlag = false;
                    isBtnGenerated = false;
                    Destroy(generatedBtn);

                    STATE = 0;
                }
                break;
        }        
    }

    // 양측 전투 준비
    void BattleReady()
    {
        // 위치로
        PC.transform.position = GameObject.Find("PlayerStartPosition").transform.position;
        EC.transform.position = GameObject.Find("EnemyStartPosition").transform.position;

        BATTLE_COUNT = 3;
        PC.state = 0;
        EC.state = 0;


        if (!isBtnGenerated)
        {
            isBtnGenerated = true;
            generatedBtn = Instantiate(Btn, BtnCase.transform.position, Quaternion.identity, BtnCase.transform);
            generatedBtn.GetComponent<HitPointController>().isDashStart = true;
        }
    }

    void BattleStart()
    {
        for (int i = BATTLE_COUNT; i > 0; i++)
        {
            EnemyTurn();
            PlayerTurn();
        }
    }

    void EnemyTurn()
    {
        // EnemyController 의 공격 함수 호출
    }


    void PlayerTurn()
    {
        // PlayerController 의 공격 함수 호출 ?
        // 일해라 철민
    }
}
