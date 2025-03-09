using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    private Vector3 walkingPoint;
    private bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInsightRange, playerInAttackRange;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        // Ensure NavMeshAgent does NOT adjust Y position
        agent.autoTraverseOffMeshLink = false;
        agent.updateUpAxis = false;

        // Force enemy to start on the ground
        EnsureGroundedPosition();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        bool isMoving = false;
        // Keep enemy locked to the ground
        LockToGround();

        // Check if player is in range
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInsightRange && !playerInAttackRange)
        {
            Patroling();
            transform.LookAt(player);
            //animator.SetBool("isMoving", true);
            
        }

        if (playerInsightRange && !playerInAttackRange)
        {
            ChasePlayer();
            transform.LookAt(player);
            animator.SetBool("isMoving", true);
        }
       // if (playerInAttackRange && playerInsightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkingPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkingPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        NavMeshHit hit;

        // Ensure the point is on the NavMesh
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            walkingPoint = hit.position;
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        if (!playerInAttackRange)  
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        // Stop movement properly without causing glitches
        agent.isStopped = true;
        transform.LookAt(player);

        if (!alreadyAttacked)
        { 
            Debug.Log("Attacking Player"); 
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        agent.isStopped = false; // Resume movement after attack
    }

    // 🔥 Keep the enemy locked to the ground at all times
    private void LockToGround()
    {
        Vector3 position = transform.position;
        position.y = GetGroundHeight();
        transform.position = position;
    }

    // ✅ Ensures enemy starts on the ground when the game starts
    private void EnsureGroundedPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = new Vector3(hit.position.x, hit.position.y, hit.position.z); // Force snap to ground
        }
    }

    // 🔍 Finds the correct Y position of the ground using a Raycast
    private float GetGroundHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, out hit, 5f, whatIsGround))
        {
            return hit.point.y; // Return the ground Y position
        }
        return transform.position.y; // Keep current position if no ground is detected
    }
}
