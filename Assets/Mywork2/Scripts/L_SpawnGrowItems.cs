using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_SpawnGrowItems : MonoBehaviour
{
    [SerializeField]
    private GameObject GrowItem;
    private GameObject GrowItems; // root parent object 

    private float timer = 0f;
    // 랜덤의 소환시간 간격 
    private float rndTime = 0f;

    // GrowItem이 소환되는 좌표의 범위 
    public float minXpos = -40;
    public float maxXpos = 160;
    public float minZpos = -40;
    public float maxZpos = 160;

    // GrowItem이 소환되는 시간 간격범위
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 1.0f;

    // 소환될수 있는 최대 GrowItem 갯수
    public int maxSpawnNums = 50;
    // 현재 소환되있는 GrowItem 갯수
    [HideInInspector]
    public int curSpawnedNums;

    private void Start()
    {
        Vector3 SpawnPos = new Vector3(1, 1, 0);

        Instantiate(GrowItem, SpawnPos, Quaternion.identity);
        GrowItems = GameObject.Find("GrowItems");

        RandomTime(); // 랜덤 소환 시간간격 부여 
    }

    private void Update()
    {

        timer += Time.deltaTime;

        // timer가 설정된 rndTIme에 도달했고 최대소환 갯수보다 적게 소환되어 있으면 GrowItem 소환, 새로운 rndTime 부여 
        if (timer >= rndTime && curSpawnedNums < maxSpawnNums)
        {
            RandomSpawn();
            RandomTime();
            timer = 0f; // timer 초기화            
        }

    }

    // 랜덤한 소환 시간간격 조성 
    private void RandomTime()
    {
        rndTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // 랜덤한 좌표에 GrowItem 하나 소환 
    private void RandomSpawn()
    {
        // xpos, zpos에 random range 부여
        float xpos = Random.Range(minXpos, maxXpos);
        float zpos = Random.Range(minZpos, maxZpos);
        Vector3 RandomSpawnPos = new Vector3(xpos, 1, zpos);

        // 랜덤한 좌표계에 GrowItem 소환 
        GameObject spawned = Instantiate(GrowItem, RandomSpawnPos, Quaternion.identity);
        // 소환된 오브젝트를 GrowItems의 child로서 소환 (하이라키에서 GrowItems의 child로서 정렬되도록)
        spawned.transform.parent = GrowItems.transform;

        // 소환 갯수 증가 
        curSpawnedNums++;
    }
}
