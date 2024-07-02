using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public string gameSceneName = "Leveldesign"; // Name of your main game scene
    public GameObject tutorialInterface; // Assign the TutorialInterface in the Inspector
    public GameObject menu;
    public GameObject logo;

    public void StartGame()
    {
        Debug.Log("Starting the game...");
        SceneManager.LoadScene(gameSceneName);
    }

    public void ShowTutorial()
    {
        if (tutorialInterface != null)
        {
            tutorialInterface.SetActive(true);
        }

        if (menu != null)
        {
            menu.SetActive(false);
        }

        if (logo != null)
        {
            logo.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
