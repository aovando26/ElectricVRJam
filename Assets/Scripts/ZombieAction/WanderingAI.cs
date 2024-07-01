using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class WanderingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    [SerializeField] private float stoppingDistance = 1f;
    public bool IsInProximity { get; private set; }
    private AnimateEnemy animateEnemy;
    private bool isAttacking = false;

    [SerializeField] private float wallCheckDistance = 1.5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float avoidanceAngle = 45f;
    [SerializeField] private float obstacleCheckRadius = 3f;

    private void Awake()
    { 
        wallLayer = LayerMask.GetMask("wallLayer");
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animateEnemy = GetComponent<AnimateEnemy>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
        }
        else if (!agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not placed on a NavMesh!");
        }
    }

    private void Update()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            if (IsNearObstacle())
            {
                CheckForWallAndAvoid();
            }
            MoveToTarget();
            UpdateWalkingAnimation();
        }
        else
        {
            Debug.LogWarning("Agent is not ready or not on a NavMesh.");
        }
    }

    private bool IsNearObstacle()
    {
        return Physics.CheckSphere(transform.position, obstacleCheckRadius, wallLayer);
    }

    private void CheckForWallAndAvoid()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, wallCheckDistance, wallLayer))
        {
            Vector3 avoidanceDirection = Quaternion.Euler(0, Random.Range(-avoidanceAngle, avoidanceAngle), 0) * -hit.normal;
            Vector3 newTargetPosition = transform.position + avoidanceDirection * 5f;

            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(newTargetPosition, out navMeshHit, 5f, NavMesh.AllAreas))
            {
                agent.SetDestination(navMeshHit.position);
            }
        }
    }

    private void MoveToTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            IsInProximity = distanceToTarget <= stoppingDistance;
            if (IsInProximity && !isAttacking)
            {
                RotateToTarget();
                agent.isStopped = true;
                StartCoroutine(StopAndAttack());
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    private void RotateToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator StopAndAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        animateEnemy.EnemyAttackAnimation();
        isAttacking = false;
    }

    private void UpdateWalkingAnimation()
    {
        if (animateEnemy != null)
        {
            bool isWalking = agent.velocity.sqrMagnitude > 0f && !IsInProximity;
            animateEnemy.UpdateWalkingAnimation(isWalking);
        }
    }
}