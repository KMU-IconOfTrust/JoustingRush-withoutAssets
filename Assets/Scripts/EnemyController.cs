using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameController GC;
    HitPointGenerator HPG;

    DashTester dashTester;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    GameObject myBody;

    [SerializeField]
    // 플레이어
    GameObject player;
    
    GameObject myWeapon;

    public int state;
    public bool isOnAct; // 무언가 동작중이라면
    public int attackPoint; // 공격 위치
    public int guardPoint; // 방어 위치(플레이어가 공격하는 위치)
    public int HP;

    // 공격 찬스는 최대 2회
    public int chance;

    private void Awake()
    {
        isOnAct = false;
        state = 0;
        chance = 0;
        GC = GameObject.Find("GameManager").GetComponent<GameController>();
        HPG = GetComponent<HitPointGenerator>();
        dashTester = gameObject.GetComponent<DashTester>();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = 6;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어를 바라본다
        //Vector3 tmp = player.transform.position + new Vector3(3, 0, 0);
        //myBody.transform.LookAt(tmp);
        myBody.transform.LookAt(player.transform);
        

        switch (state)
        {
            case 0:
                dashTester.DashStop(); // 정지상태
                break;
            case 1:
                dashTester.DashStart(); // 속도 설정 : 출발
                break;
            case 2:
                break;
            case 3:
                // 공격
                if (!isOnAct && chance == 0)
                {
                    enemyTurn();
                }
                break;
            case 4:
                if (isOnAct)
                {
                    enemyTurn(false);
                }
                break;
            case 5:
                break;
            case 6: break;
        }

        GameObject.Find("UIController").GetComponent<UIController>().Enemy_UpdateLife(HP);
    }

    
    void enemyTurn(bool isTurnForAttack = true)
    {
        if (state == 3)
            isOnAct = true;
        else if (state == 4)
            isOnAct = false;

        // 공격
        if (isTurnForAttack)
        {
            // 플레이어 방패 위치 확인
            // 해당 위치 제외 , 공격 위치 결정
            // 공격 진행
            // 공격 성공/실패 여부 판단 및 턴 종료

            attackPoint = Random.Range(0, 3); // 0 ~ 2
            
            //if : attackPoint가 플레이어가 방어하고있는 위치라면
            //다시 Random.Range(0,3)
            
            // 공격 
            attack(attackPoint);
        }
        //방어
        // 화면에 플레이어가 공격할 위치와 방법이 표시됨
        else
        {
            // 플레이어가 공격할 위치를 화면에 띄워줌
            // 플레이어의 공격이 잘못 이루어지면
            // 방어한것으로 간주 및 공격권 가져옴
            // 공격이 제대로 이루어지면 최대 2회까지 연속 방어 턴 진행
            // HPG.callHitPoint();
            //guardPoint = Random.Range(0, 3); // 0 ~ 2
            
            guardPoint = HPG.callHitPoint();
            guard(guardPoint);
            
        }
    }

    // attackPoint: 0-high / 1-middle / 2-low
    void attack(int attackPoint)
    {
        // 각상황에 맞는 anim 진행
        switch (attackPoint)
        {
            case 0:
                // 상단공격 anim
                Debug.Log("Enemy 상단공격");
                animator.SetTrigger("IsAttackUp");

                StartCoroutine("isAnimEndCheck");
                break;
            case 1:
                // 중단공격 anim
                Debug.Log("Enemy 중단공격");
                animator.SetTrigger("IsAttackMiddle");

                StartCoroutine("isAnimEndCheck");
                break;
            case 2:
                // 하단공격 anim
                Debug.Log("Enemy 하단공격");
                animator.SetTrigger("IsAttackDown");

                StartCoroutine("isAnimEndCheck");
                break;
        }
    }
    
    IEnumerator isAnimEndCheck()
    {
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(1).IsName("Sword_Attack_R"))
            {
                if (0.2f > animator.GetCurrentAnimatorStateInfo(1).normalizedTime)
                {
                    animator.speed = 0.2f;
                }
                else if (0.3f < animator.GetCurrentAnimatorStateInfo(1).normalizedTime)
                {
                    animator.speed = 1f;

                    Debug.Log("피격!");
                    player.GetComponent<PlayerController>().isAttacked = true;
                    StopCoroutine("isAnimEndCheck");
                    animator.Play("idle", 1);
                }
            }
            else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Sword_Attack_R_Whirlwind"))
            {
                if (0.2f > animator.GetCurrentAnimatorStateInfo(1).normalizedTime)
                {
                    animator.speed = 0.2f;
                }
                else  if (0.25f < animator.GetCurrentAnimatorStateInfo(1).normalizedTime)
                {
                    animator.speed = 1f;

                    Debug.Log("피격!");
                    player.GetComponent<PlayerController>().isAttacked = true;
                    StopCoroutine("isAnimEndCheck");
                    animator.Play("idle", 1);
                }
            }
            else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Sword_Attack_Rd"))
            {
                if (0.2f > animator.GetCurrentAnimatorStateInfo(1).normalizedTime)
                {
                    animator.speed = 0.2f;
                }
                else if (0.3f < animator.GetCurrentAnimatorStateInfo(1).normalizedTime)
                {
                    animator.speed = 1f;
                    Debug.Log("피격!");
                    player.GetComponent<PlayerController>().isAttacked = true;
                    StopCoroutine("isAnimEndCheck");
                    animator.Play("idle", 1);
                }
            }

            yield return null;
        }

        
    }

    void guard(int guardPoint)
    {
        // 각상황에 맞는 anim 진행
        switch (guardPoint)
        {
            case 0:
                // 상단공격 anim
                Debug.Log("Enemy의 상단을 공격하세요");
                break;
            case 1:
                // 중단공격 anim
                Debug.Log("Enemy의 중단을 공격하세요");
                break;
            case 2:
                // 하단공격 anim
                Debug.Log("Enemy의 하단을 공격하세요");
                break;
        }
    }
}
