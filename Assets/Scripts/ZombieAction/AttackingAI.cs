using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingAI : MonoBehaviour
{
    public int damage = 5;
    public int heal = 1; 


    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            player.Heal(heal);
        }
    }
}