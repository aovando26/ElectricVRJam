using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float electricity = 50f;
    public float maxCharge = 100.0f;
    public static event Action OnElectricityReady;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize player variables here if needed
    }

    // Update is called once per frame
    void Update()
    {
        // Update player variables based on game logic
    }

    // Method to decrease player's health
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            // Player dies
            Die();
        }
    }

    // Method to increase player's health
    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, 100);
    }

    // Method to decrease player's electricity
    public void UseElectricity(float amount)
    {
        electricity -= amount;
        electricity = Mathf.Clamp(electricity, 0f, maxCharge);
        Debug.Log("Used Electricity: " + electricity);
    }

    // Method to increase player's electricity
    public void ChargeElectricity(float amount)
    {
        if (electricity < maxCharge)
        {
            electricity += amount;
            electricity = Mathf.Clamp(electricity, 0f, maxCharge);
            Debug.Log("Current Electricity: " + electricity);
            if (electricity == maxCharge)
            {
                ElectricityReady();
            }
        }
    }

    public void ElectricityReady()
    {
        Debug.Log("Vibration has been invoked");
        OnElectricityReady.Invoke();
    }   

    // Method called when player dies
    void Die()
    {
        Debug.Log("Player died!");
    }
}