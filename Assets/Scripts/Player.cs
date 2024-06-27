using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float electricity = 50f;
    public float maxCharge = 100.0f;
    public Slider electricitySlider;
    public float electricityStart = 0.0f; 

    // used to notify listeners when the player's electricity is fully charged.
    public static event Action OnElectricityReady;

    // Start is called before the first frame update
    void Start()
    {
        if (electricitySlider != null)
        {
            electricitySlider.value = electricityStart / maxCharge;
            // Debug.Log(electricitySlider.value);
        }

        else 
        {
            Debug.Log("Slider is missing");    
        }

    }

    // Update is called once per frame
    void Update()
    {
        electricitySlider.value = electricity / maxCharge;
        // Debug.Log(electricitySlider.value);
        // Update player variables based on game logic
    }

    // Method to decrease player's health
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"Health : {health}");
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
                // called when maxCharge is reached
                ElectricityReady();
            }
        }
    }

    public void ElectricityReady()
    {
        if (OnElectricityReady != null)
        {
            Debug.Log("Vibration has now been invoked");

            // check for subscribes to the OnElectricityReady event
            // refer to ElectricityReady class for example 
            OnElectricityReady.Invoke();
        }

    }

    // Method called when player dies
    void Die()
    {
        Debug.Log("Player died!");
    }
}
