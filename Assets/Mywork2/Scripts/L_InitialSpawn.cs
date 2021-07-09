using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_InitialSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject GrowItem;
    [SerializeField]
    private GameObject GrowItems; // root parent object 

    // GrowItem이 소환되는 좌표의 범위 
    public float minXpos = -40;
    public float maxXpos = 160;
    public float minZpos = -40;
    public float maxZpos = 160;

    // 최초 소환되는 GrowItem 갯수
    public int maxSpawnNums = 50;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for(int i = 0; i < maxSpawnNums; i++)
        {
            // xpos, zpos에 random range 부여
            float xpos = Random.Range(minXpos, maxXpos);
            float zpos = Random.Range(minZpos, maxZpos);
            Vector3 RandomSpawnPos = new Vector3(xpos, 1, zpos);

            // 랜덤한 좌표계에 GrowItem 소환 
            GameObject spawned = Instantiate(GrowItem, RandomSpawnPos, Quaternion.identity);
            // 소환된 오브젝트를 GrowItems의 child로서 소환 (하이라키에서 GrowItems의 child로서 정렬되도록)
            spawned.transform.parent = GrowItems.transform;
        }

        
    }

}
