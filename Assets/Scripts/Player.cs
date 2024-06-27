using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro; // Add this for TextMeshPro
using UnityEngine.SceneManagement; // Add this for scene management

public class Player : MonoBehaviour
{
    public int health = 100;
    public float electricity = 50f;
    public float maxCharge = 100.0f;
    public Slider electricitySlider;
    public float electricityStart = 0.0f;
    public static event Action OnElectricityReady;

    public TextMeshProUGUI gameOverText;
    public Image blackOverlay;
    public float resetDelay = 5f;

    void Start()
    {
        if (electricitySlider != null)
        {
            electricitySlider.value = electricityStart / maxCharge;
        }
        else
        {
            Debug.LogError("Electricity Slider is missing");
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over Text is missing");
        }

        if (blackOverlay != null)
        {
            blackOverlay.color = new Color(0, 0, 0, 0);
        }
        else
        {
            Debug.LogError("Black Overlay is missing");
        }
    }

    void Update()
    {
        electricitySlider.value = electricity / maxCharge;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"Health : {health}");
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, 100);
    }

    public void UseElectricity(float amount)
    {
        electricity -= amount;
        electricity = Mathf.Clamp(electricity, 0f, maxCharge);
        Debug.Log("Used Electricity: " + electricity);
    }

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
        OnElectricityReady?.Invoke();
        Debug.Log("Vibration has now been invoked");
    }

    void Die()
    {
        Debug.Log("Player died!");
        StartCoroutine(GameOver());
    }

    System.Collections.IEnumerator GameOver()
    {
        if (blackOverlay != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / 1f);
                blackOverlay.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }

        if (gameOverText != null)
        {
            gameOverText.text = "You've failed! The gods are now being hunted down by your enemy.";
            gameOverText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(resetDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}