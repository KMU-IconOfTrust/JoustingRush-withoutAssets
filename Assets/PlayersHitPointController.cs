using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHitPointController : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    GameObject playerCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            playerCanvas.transform.LookAt(enemy.transform);
            playerCanvas.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
