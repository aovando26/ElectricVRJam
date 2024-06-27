using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Add this for TextMeshPro

public class OpenDoor : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    private Player player;
    public GameObject door;
    public GameObject scoreEarnedUI;
    public TextMeshProUGUI scoreWarningText;
    public int requiredSouls = 10;

    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door GameObject is not assigned in the Inspector.");
        }

        if (scoreWarningText == null)
        {
            Debug.LogError("Score Warning Text is not assigned in the Inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int currentScore = ScoreManager.instance.GetScore();

        if (currentScore >= requiredSouls)
        {
            door.SetActive(false);
            Debug.Log("Door is now open.");
        }
        else
        {
            int soulsNeeded = requiredSouls - currentScore;
            string warningMessage = $"You need {soulsNeeded} more soul{(soulsNeeded != 1 ? "s" : "")} to open the door.";

            Debug.Log(warningMessage);

            if (scoreWarningText != null)
            {
                scoreWarningText.text = warningMessage;
            }

            scoreEarnedUI.SetActive(true);
            StartCoroutine(HideScoreWarningAfterSeconds(2f));
        }
    }

    private IEnumerator HideScoreWarningAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scoreEarnedUI.SetActive(false);
    }
}