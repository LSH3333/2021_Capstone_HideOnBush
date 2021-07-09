using UnityEngine;
using UnityEngine.AI;

public class L_EnemyMovement : MonoBehaviour
{
    Transform _player;
    NavMeshAgent _nav;
    
    // Player가 범위내로 들어오면 true 
    public bool detectedPlayer = false;

    private void Awake()
    {
        // player를 찾아 네비게이션의 타겟으로 잡으려 한다
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        // Navigation agent를 참조시킨다
        _nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Debug.Log(detectedPlayer);
        // Player가 범위내로 들어오면 
        if (detectedPlayer)
        {
            // 매 프레임마다 플레이어의 위치로 네비게이션 목적직로 지정
            _nav.SetDestination(_player.position);
        }
    }

    // Sphere Collider 진입
    private void OnTriggerEnter(Collider other)
    {
        // Sphere안에 들어온 오브젝트가 Player라면
        if (other.tag == "Player")
        {
            detectedPlayer = true;
        }
        
    }

    // Sphere Collder 탈출
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // Player가 범위 밖으로 빠져나가면 detectedPlayer를 false로해서 추적을 멈춤
            detectedPlayer = false;
        }
    }

}
