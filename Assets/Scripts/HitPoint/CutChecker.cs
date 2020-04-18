using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutChecker : MonoBehaviour
{
    HitPointController HPC;

    private void Awake()
    {
        HPC = transform.parent.parent.GetComponent<HitPointController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            //무언가 버튼들이라면. 바로 넘김
            if (HPC.isMainMenu || HPC.isDashStart || HPC.reBattle) {
                HPC.returnResult(true);
            }
            else
            {
                HPC.checkCollectHit(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
