using UnityEngine;

public class L_SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject slime;
    private GameObject SpawnedEnemies; // root parent object

    private float timer = 0f;
    // 랜덤의 소환시간 간격 
    private float rndTime = 0f;

    // Enemy가 소환되는 좌표의 범위 
    public float minXpos = -40;
    public float maxXpos = 160;
    public float minZpos = -40;
    public float maxZpos = 160;

    // Enemy 소환되는 시간 간격범위
    public float minSpawnTime = 0.5f;
    public float maxSpawnTime = 1.0f;

    // 소환될수 있는 최대 Enemy 갯수
    public int maxSpawnNums = 50;
    // 현재 소환되있는 Enemy 갯수
    [HideInInspector]
    public int curSpawnedNums;

    // 소환되는 Enemy의 최소크기, 최대크기 
    public float minScale = 1f;
    public float maxScale = 5f;

    private void Start()
    {
        Vector3 SpawnPos = new Vector3(1, 1, 0);

        Instantiate(slime, SpawnPos, Quaternion.identity);
        SpawnedEnemies = GameObject.Find("SpawnedEnemies");

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

    // 랜덤한 좌표에 enemy 하나 소환 
    private void RandomSpawn()
    {
        // xpos, zpos에 random range 부여
        float xpos = Random.Range(minXpos, maxXpos);
        float zpos = Random.Range(minZpos, maxZpos);
        Vector3 RandomSpawnPos = new Vector3(xpos, 3f, zpos);
        Debug.Log(RandomSpawnPos);

        // 랜덤한 좌표계에 Enemy 소환 
        GameObject spawned = Instantiate(slime, RandomSpawnPos, Quaternion.identity);
        /*Vector3 currentPos = spawned.transform.position;
        currentPos.y = currentPos.y + 3.0f;
        spawned.transform.position = currentPos;*/

        
        // 랜덤 크기의 enemy 소환 
        slime.transform.localScale = getRandomScale();
        
        
        // 소환된 오브젝트를 SpawnedEnemies 게임오브젝트의 child로서 소환 (하이라키에서 SpawnedEnemies의 child로서 정렬되도록)
        spawned.transform.parent = SpawnedEnemies.transform;

        // 소환 갯수 증가 
        curSpawnedNums++;
    }

    // Random의 vector3 값 반환 
    private Vector3 getRandomScale()
    {
        float randScale = Random.Range(minScale, maxScale);
        return new Vector3(randScale, randScale, randScale);
    }
}
