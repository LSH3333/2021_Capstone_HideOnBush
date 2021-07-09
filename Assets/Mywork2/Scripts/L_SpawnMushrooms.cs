using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_SpawnMushrooms : MonoBehaviour
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

    // mushroom 소환할 좌표 
    Vector3 RandomSpawnPos = Vector3.zero;
    // Ground Layer 구분을 위한 LayerMask 
    public LayerMask whatIsGround;
    // 소환할 좌표가 정해졌는지 
    private bool spawnPointSet = false;

    private void Start()
    {
        /*Vector3 SpawnPos = new Vector3(1, 1, 0);

        Instantiate(GrowItem, SpawnPos, Quaternion.identity);*/
        GrowItems = GameObject.Find("GrowItems");

        RandomTime(); // 랜덤 소환 시간간격 부여 
    }

    private void Update()
    {
        // 타이머
        timer += Time.deltaTime;
        // 소환할 좌표가 정해지지 않았다면 좌표를 정한다 
        if(!spawnPointSet) SearchSpawnPoint();

        // timer가 설정된 rndTIme에 도달했고 최대소환 갯수보다 적게 소환되어 있으면 GrowItem 소환, 새로운 rndTime 부여 
        // && spawnPointSet이 true 라면 ( 소환할 좌표가 정해졌다면 )
        if (timer >= rndTime && curSpawnedNums < maxSpawnNums && spawnPointSet)
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
        //if (!spawnPointSet) SearchSpawnPoint();

        // 랜덤한 좌표계에 GrowItem 소환 
        GameObject spawned = Instantiate(GrowItem, RandomSpawnPos, Quaternion.identity);
        // 소환된 오브젝트를 GrowItems의 child로서 소환 (하이라키에서 GrowItems의 child로서 정렬되도록)
        spawned.transform.parent = GrowItems.transform;

        // 소환 갯수 증가 
        curSpawnedNums++;
        spawnPointSet = false; // 소환 완료 했으므로 spawnPointSet은 false로 되돌림 
    }

    void SearchSpawnPoint()
    {
        // xpos, zpos에 random range 부여
        float xpos = Random.Range(minXpos, maxXpos);
        float zpos = Random.Range(minZpos, maxZpos);
        RandomSpawnPos = new Vector3(xpos, 1, zpos);
        RaycastHit groundHit;

        // 랜덤포지션에서 아래로 ray를 쏴서 ground라면 
        if (Physics.Raycast(RandomSpawnPos, -transform.up, out groundHit, 2f, whatIsGround))
        {
            // ray를 맞은 ground의 y좌표에 소환한다 ( 땅에 붙어서 mushroom이 소환되도록)
            RandomSpawnPos.y = groundHit.point.y;
            spawnPointSet = true; 
        }
    }
}
