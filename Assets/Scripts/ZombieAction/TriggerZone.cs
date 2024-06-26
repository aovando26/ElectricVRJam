using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public Transform playerTarget; // Assign XR Origin target here

    private void OnTriggerEnter(Collider other)
    {
        WanderingAI wanderingAI = other.GetComponent<WanderingAI>();
        MoveForward moveForward = other.GetComponent<MoveForward>();

        if (wanderingAI != null && moveForward != null)
        {
            moveForward.StopMoving(); // Stop the MoveForward script
            moveForward.enabled = false; // Disable MoveForward script
            wanderingAI.target = playerTarget; // Set the target for WanderingAI
            wanderingAI.enabled = true; // Enable the WanderingAI script
        }
    }
}
