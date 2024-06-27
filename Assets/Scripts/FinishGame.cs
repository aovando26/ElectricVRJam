using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class FinishGame : MonoBehaviour
{
    public float deadTime = 1.0f;
    private bool _deadTimeActive = false;
    public UnityEvent onPressed, onReleased;

    public AudioSource soundEffect;
    public Image blackOverlay;
    public TextMeshProUGUI endText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button") && !_deadTimeActive)
        {
            onPressed?.Invoke();
            FinishTheGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button") && !_deadTimeActive)
        {
            onReleased?.Invoke();
            StartCoroutine(WaitForDeadTime());
        }
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }

    private void FinishTheGame()
    {
        Debug.Log("Button is pressed, game is finished!");

        if (soundEffect != null)
        {
            soundEffect.Play();
        }

        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        if (blackOverlay != null)
        {
            float elapsedTime = 0f;
            Color originalColor = blackOverlay.color;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / 1f);
                blackOverlay.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }

        if (endText != null)
        {
            endText.text = "You did it! You've successfully warned the other gods, you have prevailed!";
            endText.gameObject.SetActive(true);
        }

        // Additional game-ending logic can be added here
    }

    void Start()
    {
        if (blackOverlay != null)
        {
            blackOverlay.color = new Color(0, 0, 0, 0);
        }

        if (endText != null)
        {
            endText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Update logic (if needed)
    }
}