using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;

    void Start()
    {

    }

    public void Pause()
    {
        pausePanel.SetActive(true); // show the pause UI
        Time.timeScale = 1.0f; // stops the game time    
    }

    public void Continue()
    {
        pausePanel.SetActive(false); // hides the pause UI
        Time.timeScale = 1.1f; // resume the game time
    }
}