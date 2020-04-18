using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTester : MonoBehaviour
{
    //[SerializeField]
    float speed = 15f;

    bool isDash;
    float bef_z, cur_z; // 직전 , 현재 z
    float controlSpeed; // BattleController에서 감속 가속 할 때 쓰이는 값

    [SerializeField]
    Animator rideAnim;

    float time;
    float allTIme;

    void Awake()
    {

        controlSpeed = 1.0f;
        isDash = false;

        bef_z = 0f;
        cur_z = 0f;

        time = 0f;
        allTIme = 0.0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        Dash();
        //Debug.Log(allTIme);
    }

    void Dash()
    {
        if (isDash)
        {
            time += Time.deltaTime;
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed * controlSpeed);
        }
    }

    public void DashStop()
    {
        isDash = false;
    }

    public void DashStart()
    {
        isDash = true;
    }

    // 감속
    public void DashSlower(float controlSpeed)
    {
        //this.controlSpeed = 1 / controlSpeed;
        //rideAnim.speed = 1 / controlSpeed * 10;

        this.controlSpeed = 0.005f;
        rideAnim.speed = 0.005f;
    }

    // 원래대로
    public void DashFaster()
    {
        this.controlSpeed = 1.0f;
        rideAnim.speed = 1.0f;
    }
}
