using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    // Start is called before the first frame update

  
    public GameObject[] Enemy_HP_Image;
    public GameObject[] Player_HP_Image;

    private void Awake()
    {
        
    }

    public void Enemy_UpdateLife(int HP) //적 HP 업데이트 EC.Update 에서 호출
    {
        for (int i = 0; i < Enemy_HP_Image.Length; i++)
        {
            if (i < HP)
                Enemy_HP_Image[i].SetActive(true);
            else
                Enemy_HP_Image[i].SetActive(false);
        }
    }

    public void Player_UpdateLife(int HP) //플레이어 HP 업데이트 PC.Update 에서 호출
    {
        for (int i = 0; i < Player_HP_Image.Length; i++)
        {
            if (i < HP)
                Player_HP_Image[i].SetActive(true);
            else
                Player_HP_Image[i].SetActive(false);
        }
    }
}
