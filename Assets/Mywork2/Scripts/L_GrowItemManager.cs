using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_GrowItemManager : MonoBehaviour
{
    private L_SpawnGrowItems _GrowItemManager;

    private void Awake()
    {
        _GrowItemManager = GameObject.Find("GameManager").GetComponent<L_SpawnGrowItems>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // GrowItem이 파괴되면 A_SpawnGrowItems 클래스의 curSpawnedNums값도 1감소 해야한다.             
            _GrowItemManager.curSpawnedNums--;


            Destroy(gameObject); // 먹었으니 파괴 

        }

    }
}
