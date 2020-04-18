using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabChecker : MonoBehaviour
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
            HPC.checkCollectHit(false);
        }
    }
    
}
