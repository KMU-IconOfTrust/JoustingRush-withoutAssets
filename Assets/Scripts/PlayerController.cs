using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // VR이기에 꺼져있음
    // 죽거나 이긴 경우 활성화 후 애니메이션 컷씬?
    [SerializeField]
    GameObject character;
    [SerializeField]
    Animator animator;

    DashTester dashTester;

    GameController GC;
    HitPointGenerator HPG; // Enemy의 히트포인트 생성기

    EffectiveSoundController ESC;
    //각 효과음 오디오 소스
    [SerializeField]
    AudioSource WeaponSound;
    [SerializeField]
    AudioSource ShieldSound;
    [SerializeField]
    AudioSource RiderSound;

    [HideInInspector]
    public int state;

    // 방패의 위치.
    // -1: NULL
    // 0: 상단 // > 2.7
    // 1: 중단 // 2.4 < yPos < 2.7
    // 2: 하단 // < 2.4
    [HideInInspector]
    public int guardPoint;
    public bool isAttacked; // 적이 공격이 들어오는 시점

    // 공격 위치
    // -1: NULL
    // 0: 상단
    // 1: 중단
    // 2: 하단
    [HideInInspector]
    public int attackPoint;
    public int attack; // -1(null) 0(false) 1(true)

    float timer;

    public int HP;

    [SerializeField]
    GameObject shield; // yPos

    private void Awake()
    {
        state = 0;
        dashTester = gameObject.GetComponent<DashTester>();

        GC = GameObject.Find("GameManager").GetComponent<GameController>();
        HPG = GameObject.Find("Enemy").GetComponent<HitPointGenerator>();
        ESC = GameObject.Find("SoundManager").GetComponent<EffectiveSoundController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        guardPoint = -1;
        isAttacked = false;
        attackPoint = -1;
        attack = -1;
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shield ypos >> " + shield.transform.position.y);


        switch (state)
        {
            case 0:
                dashTester.DashStop();
                break;
            case 1:
                dashTester.DashStart();
                break;
            case 2:
                break;
            case 3:
                // 방어
                playerTurn(false);
                break;
            case 4:
                // 공격
                playerTurn(true);
                break;
            case 5:
                break;
            case 6:
                break;
        }
        GameObject.Find("UIController").GetComponent<UIController>().Player_UpdateLife(HP);
    }

    void playerTurn(bool isTurnForAttack = true)
    {

        // 공격
        if (isTurnForAttack)
        {
            timer += Time.deltaTime;
            //Debug.Log("공격 위치 생각중");

            if (Input.GetKeyDown(KeyCode.E))
                attackPoint = 0;
            else if (Input.GetKeyDown(KeyCode.D))
                attackPoint = 1;
            else if (Input.GetKeyDown(KeyCode.C))
                attackPoint = 2;

            // 공격 입력이 들어왔다면.
            // attack이 -1(null)가 아닌 경우 :   HitPoint에 조작이 가해진 경우
            // attackPoint가 -1이 아닌 경우 : 키보드로 입력이 들어온 경우
            // 그 외 시간이 초과된 경우에도 HitPoint 삭제
            if (attack != -1 || attackPoint != -1 || timer>2f)
            //if (attack != -1 || attackPoint != -1 )
            {
                timer = 0f;

                GC.waitPlayerAct = false;
                HPG.eraseHitPoint();
            }
        }

        // 방어
        else
        {
            //Debug.Log("방어 위치 생각중");

            //if(shield.transform.position.y > 2.7f)
            //if (shield.transform.position.y > 2.1f)
            if (shield.transform.position.y > 2.2f)
            {
                guardPoint = 0; // 상
            }
            //else if (shield.transform.position.y < 2.4f)
            //else if (shield.transform.position.y < 1.9f)
            else if (shield.transform.position.y < 1.8f)
            {
                guardPoint = 2; // 하
            }
            else
            {
                guardPoint = 1; // 중
            }

            //if (Input.GetKeyDown(KeyCode.Q))
            //    guardPoint = 0;
            //else if (Input.GetKeyDown(KeyCode.A))
            //    guardPoint = 1;
            //else if (Input.GetKeyDown(KeyCode.Z))
            //    guardPoint = 2;
            
            // 방어 입력이 들어왔다면.
            // Enemy의 공격 위치와 Player의 방어 위치가
            // GameController로 전달.
            //if(guardPoint != -1)

            // 적의 공격이 들어왔다면 ( 피격 시점 )
            if(isAttacked)
            {
                timer = 0f;
                GC.waitPlayerAct = false;
                isAttacked = false;
            }
        }
    }


    public void soundPlayer(int type)
    // type
    // 0 : WeaponSound
    // 1 : ShieldSound
    // 2 : RiderSound
    {
        switch (type)
        {
            case 0:
                WeaponSound.clip = ESC.weaponSound;
                WeaponSound.Play();
                break;

            case 1:
                ShieldSound.clip = ESC.shieldSound;
                ShieldSound.Play();
                break;

            case 2:
                RiderSound.clip = ESC.riderSound;
                RiderSound.Play();
                break;
        }
        
    }

    public void deadAnim()
    {
        character.SetActive(true);
        animator.speed = 0.3f;
        animator.SetTrigger("IsDeath");
        StartCoroutine("_deadAnim");
    }

    IEnumerator _deadAnim()
    {
        while (true)
        {
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            // 죽는 애니메이션의 80% 이상이 진행되었다면
            if (0.8f < animator.GetCurrentAnimatorStateInfo(0).normalizedTime  && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                SceneManager.LoadScene(1);
            }

            character.transform.Translate(new Vector3(100 * Time.deltaTime, 0, 0));
            yield return null;
        }
    }


    public void winAnim()
    {
        character.SetActive(true);
        //animator.SetTrigger( ... );
    }
    
}
