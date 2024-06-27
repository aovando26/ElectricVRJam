using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target; // refer to line 34 of spawn manager
    [SerializeField] private float stoppingDistance = 1f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // frame by frame when spawned 
    private void Update()
    {
        MoveToTarget();
    }

    public void MoveToTarget()
    {
        if (target != null)
        {
            // follows the xr origin position 
            agent.SetDestination(target.position);

            // return the distance between a and b and stores in variable
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

    // method to smoothly rotate an object to face a target position 
    private void RotateToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
