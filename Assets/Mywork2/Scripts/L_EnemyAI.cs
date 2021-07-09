using UnityEngine;
using UnityEngine.AI;

// 패트롤하다가 Player가 sightRange안에 들어오면 Chase.
public class L_EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    // Player, Ground Layer구분위한 LayerMask
    public LayerMask whatIsGround, whatIsPlayer;

    // patroling
    public Vector3 walkPoint; // Patrol 향할 지점 
    bool walkPointSet;
    public float walkPointRange; // walkPoint가될 범위 지정 

    // attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;

    // States
    public float sightRange; // 이 범위 안에 player가 들어오면 쫒아감 
    public float attackRange; // attack 하게되면 활성화 
    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange;

    private Animator _ani;

    public GameObject CUBE;

    private void Awake()
    {
        //player = GameObject.Find("MinimalCharacter").transform;
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _ani = GetComponent<Animator>();
        
    }

    private void Update()
    {
        Debug.Log(L_BushHide.PlayerInBush);
        // Player가 Bush밖에 있다
        if(!L_BushHide.PlayerInBush)
        {            
            // check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        }
        else // Player가 Bush 안에 있다
        {
            // playerInSightRange를 false로 함으로서 Slime이 더이상 Player를 쫒지 않고 
            // 다시 Patrol 상태로 돌아감. 
            playerInSightRange = false;
        }

        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false; // find new walkPoint
    }

    private void SearchWalkPoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        
        // downRay는 walkPoint에서 조금 y축으로 떨어진곳
        Vector3 downRay = new Vector3(walkPoint.x, walkPoint.y + 3f, walkPoint.z);
        //GameObject _CUBE = Instantiate(CUBE, downRay, Quaternion.identity); // 만약 빛이 내려가다가 ground아닌것을 만나면 다시

        // walkPoint에서 y축으로 조금 떨어진곳에서 아래로 ray를 쏴서
        // 만약 그곳이 "unground layer"라면 walkPoint를 다시 계산 
        RaycastHit info;
        if(Physics.Raycast(downRay, -transform.up, out info))
        {
            if(info.transform.tag == "unground layer")
            {
                //_CUBE.GetComponent<MeshRenderer>().material.color = Color.red;
                return;
            }
        }

        // check if walkPoint is on the ground 
        // walkPoint에서 아래방향으로 2f만큼 ray를 쏴서 Ground인지 확인. 
        if (Physics.Raycast(downRay, -transform.up, 10f, whatIsGround))
            walkPointSet = true;
        

    }

    private void ChasePlayer()
    {
        _ani.SetTrigger("Walk");
        agent.SetDestination(player.position);
    }

    /*
    private void AttackPlayer()
    {
        // enemy가 움직이지 않아야함
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            // attack code here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    */


}
