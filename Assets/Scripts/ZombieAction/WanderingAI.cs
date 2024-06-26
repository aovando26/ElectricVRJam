using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; // This will be assigned dynamically
    [SerializeField] private float stoppingDistance = 3f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= stoppingDistance)
            {
                RotateToTarget();
                agent.isStopped = true; // Stop the NavMeshAgent when reaching the target
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
}
