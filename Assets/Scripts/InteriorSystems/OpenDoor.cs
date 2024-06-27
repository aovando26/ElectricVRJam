using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenDoor : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    private Player player;
    //private bool doorOpen = false;
    public GameObject door;
    public GameObject scoreEarnedUI;

    void Start()
    {
        // ensure the door GameObject is assigned
        if (door == null)
        {
            Debug.LogError("Door GameObject is not assigned in the Inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // !doorOpen could be a condition as well

        if (ScoreManager.instance.GetScore() >= 10)
        {
            door.SetActive(false);
            //doorOpen = true;
            Debug.Log("Door is now open.");
        }
        else
        {
            Debug.Log("You do not have enough points to open the door");
            scoreEarnedUI.SetActive(true);
            StartCoroutine(HideScoreWarningAfterSeconds(1.5f));
        }
    }

    private IEnumerator HideScoreWarningAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scoreEarnedUI.SetActive(false);
    }
}

