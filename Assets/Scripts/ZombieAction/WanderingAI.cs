using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class WanderingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; // refer to line 34 of spawn manager
    [SerializeField] private float stoppingDistance = 1f;
    public bool IsInProximity { get; private set; }
    private AnimateEnemy animateEnemy;
    private bool isAttacking = false;

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
            MoveToTarget();
            UpdateWalkingAnimation();
        }
        else
        {
            Debug.LogWarning("Agent is not ready or not on a NavMesh.");
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
                agent.isStopped = true; // Stop the NavMeshAgent when reaching the target
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
        yield return new WaitForSeconds(1f); // Wait for 1 second
        animateEnemy.EnemyAttackAnimation();
        isAttacking = false;
    }

    private void UpdateWalkingAnimation()
    {
        // Update walking animation based on NavMeshAgent's velocity
        if (animateEnemy != null)
        {
            bool isWalking = agent.velocity.sqrMagnitude > 0f && !IsInProximity;
            animateEnemy.UpdateWalkingAnimation(isWalking);
        }
    }
}
