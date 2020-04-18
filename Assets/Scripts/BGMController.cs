using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    // bgm 음원
    // 배틀 중에만(슬로우 중) 재생
    AudioSource battleAudio;
    [SerializeField]
    AudioClip beforeBattleSound;
    [SerializeField]
    AudioClip battleSound;

    private void Awake()
    {
        battleAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        battleAudio.clip = beforeBattleSound;
        battleAudio.Play(); // 전투 전 bgm 시작
    }

    public void bgmOn()
    {
        battleAudio.clip = battleSound;
        battleAudio.Play();
    }

    public void bgmOff()
    {
        battleAudio.clip = beforeBattleSound;
        battleAudio.Play();
        
    }

}
