using UnityEngine;

public class L_RandomPatrol : MonoBehaviour
{
    // 이동 속도
    public float speed;

    private float waitTime;
    // patrol 지점 도착해서 멈춰있을 시간 
    public float startWaitTime;

    // patrol 향할 지점의 transform
    public Transform moveSpot;

    // patrol 지점 좌표의 점위 
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // A_EnemyMovement의 
    [HideInInspector]
    public bool _detectedPlayer;
    public L_EnemyMovement _enemymovement;

    private void Start()
    {
        //_enemymovement = GetComponent<A_EnemyMovement>();

        waitTime = startWaitTime;

        moveSpot.position = new Vector3(Random.Range(minX, maxX), 1f, Random.Range(minZ, maxZ));
        _detectedPlayer = _enemymovement.detectedPlayer;

    }

    private void Update()
    {
        if (_detectedPlayer) return;

        transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        // patrol 지점으로 고개를 돌림
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveSpot.position), 0.15f);
        transform.LookAt(moveSpot);

        // 도착지점 근처까지 도착 
        if (Vector2.Distance(transform.position, moveSpot.position) < 2.0f)
        {
            if (waitTime <= 0)
            {
                // 새로운 좌표로 이동하도록 
                moveSpot.position = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
                // waitTime 갱신 
                waitTime = startWaitTime;
            }
            else
            {
                // patrol 지점으로 이동중일때는 시간계속 감소하도록 
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

}
