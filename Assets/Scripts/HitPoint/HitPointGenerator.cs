using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointGenerator : MonoBehaviour
{
    // 랜덤으로 상 중 하 중에서 한 곳에
    // 랜덤으로 찌르기 베기 중 하나를 출력

    [SerializeField]
    //GameObject[] HitPoint; // 찌르기, 베기
    GameObject HitPoint; // 베기

    [SerializeField]
    GameObject[] HitArea; // 상단 중단 하단

    [SerializeField]
    GameObject EnemyCanvas; // HitPoint를 이 캔버스의 자식으로 설정

    GameObject curHitPoint; // 생성된 히트 포인트
    

    // 생성
    public int callHitPoint()
    {
        //int hitPoint = Random.Range(0, 2); // 0 or 1
        int hitArea = Random.Range(0, 3); // 0 or 1 or 2
        float widthRange = Random.Range(-0.3f, 0.3f);
        Debug.Log("생성");
        //curHitPoint = Instantiate(HitPoint[hitPoint], HitArea[hitArea].transform.position, Quaternion.identity, EnemyCanvas.transform);
        Vector3 tmp = HitArea[hitArea].transform.position;
        //curHitPoint = Instantiate(HitPoint[hitPoint], HitArea[hitArea].transform.position, EnemyCanvas.transform.rotation, EnemyCanvas.transform);
        //curHitPoint = Instantiate(HitPoint[hitPoint], tmp, EnemyCanvas.transform.rotation, EnemyCanvas.transform);

        //curHitPoint = Instantiate(HitPoint, tmp, EnemyCanvas.transform.rotation, EnemyCanvas.transform);
        float rotY = EnemyCanvas.transform.parent.rotation.eulerAngles.y; // 캔버스 따라 돌게
        float rotZ = Random.Range(0, 360); // 베는 방향 회전시키기w

        Debug.Log(EnemyCanvas.transform.parent.rotation.eulerAngles.y);
        curHitPoint = Instantiate(HitPoint, tmp, Quaternion.Euler(new Vector3(0, rotY, rotZ)), EnemyCanvas.transform);
        Debug.Log(curHitPoint.transform.rotation.y);

        curHitPoint.transform.Translate(new Vector3(widthRange, 0, 0));

        // 0: 상단
        // 1: 중단
        // 2: 하단
        return hitArea;
    }

    public void eraseHitPoint()
    {
        Destroy(curHitPoint);
    }
}
