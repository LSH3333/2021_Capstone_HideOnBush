using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 시작시 오브젝트들 소환 (mushrooms, bushes) 
public class L_InitialSpawnObjects : MonoBehaviour
{
    [SerializeField]
    private GameObject GrowItem; // mushroom
    private GameObject GrowItems; // root parent object 

 
    // GrowItem이 소환되는 좌표의 범위 
    public float minXpos = -40;
    public float maxXpos = 160;
    public float minZpos = -40;
    public float maxZpos = 160;

    // 소환될수 있는 최대 GrowItem 갯수
    public int maxMushroomSpawnNums = 50;
    public int maxBushSpawnNums = 40;

    // mushroom, bush 소환할 좌표 
    Vector3 RandomSpawnPos = Vector3.zero;
    

    // Ground Layer 구분을 위한 LayerMask 
    public LayerMask whatIsGround;
    // 소환할 좌표가 정해졌는지 
    private bool spawnPointSet = false;


    // Bushes
    [SerializeField]
    private GameObject bush1; // big bush 
    [SerializeField]
    private GameObject bush2; // small bush
    private GameObject bush; // what i will actually spawn 
    private GameObject Bushes; // parent object of bush (root)
    

    private void Start()
    {        
        // 하이라키에서 소환물들이 부모오브젝트의 차일드로서 정렬되도록 
        GrowItems = GameObject.Find("GrowItems");
        Bushes = GameObject.Find("Bushes");
        
        // spawn mushrooms
        for(int i = 0; i < maxMushroomSpawnNums; i++)
        {
            SearchSpawnPoint();
            RandomSpawn(GrowItem);
        }

        for(int i = 0; i < maxBushSpawnNums; i++)
        {
            SearchSpawnPoint();
            DecideBush();
            RandomSpawn(bush);
        }

    }

    // bush1, bush2 중 하나 정함 
    private void DecideBush()
    {
        // bush1 : bush2 = 1 : 2
        int randNum = Random.Range(0, 3);
        if(randNum == 0)
        {
            bush = bush1;
        }
        else
        {
            bush = bush2;
        }
    }

    // 랜덤한 좌표에 GrowItem 하나 소환 
    private void RandomSpawn(GameObject spawnObject)
    {
        // 랜덤한 좌표계에 GrowItem 소환 
        GameObject spawned = Instantiate(spawnObject, RandomSpawnPos, Quaternion.identity);
        // 소환된 오브젝트를 GrowItems의 child로서 소환 (하이라키에서 GrowItems의 child로서 정렬되도록)
        
        // 소환 오브젝트가 mushroom 이라면 GrowItems의 차일드로서 소환 
        if(spawnObject == GrowItem)
        {
            spawned.transform.parent = GrowItems.transform;
        }
        else if(spawnObject == bush) // bush라면 Bushes의 차일드로
        {
            spawned.transform.parent = Bushes.transform;
        }

        spawnPointSet = false; // 소환 완료 했으므로 spawnPointSet은 false로 되돌림 
    }

    void SearchSpawnPoint()
    {
        while(!spawnPointSet)
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

    
}
