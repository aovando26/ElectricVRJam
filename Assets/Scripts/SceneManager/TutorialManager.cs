using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public string gameSceneName = "Leveldesign"; // Name of your main game scene

    // List of tutorial steps (deactivated game objects)
    public GameObject[] tutorialSteps;

    private int currentStepIndex = 0;

    // Assign these in the Inspector
    public GameObject tutorialInterface;
    public GameObject canvas;
    public GameObject panelImage;
    public GameObject skipButton;
    public GameObject nextButton;

    private void OnEnable()
    {
        currentStepIndex = 0;
        ShowCurrentStep();
    }

    public void NextStep()
    {
        // Deactivate the current step
        if (currentStepIndex < tutorialSteps.Length)
        {
            tutorialSteps[currentStepIndex].SetActive(false);
        }

        // Move to the next step
        currentStepIndex++;

        // If we have more steps, show the next step
        if (currentStepIndex < tutorialSteps.Length)
        {
            ShowCurrentStep();
        }
        else
        {
            // If no more steps, start the game
            StartGame();
        }
    }

    public void SkipTutorial()
    {
        // Skip the tutorial and start the game
        StartGame();
    }

    private void ShowCurrentStep()
    {
        if (currentStepIndex < tutorialSteps.Length)
        {
            // Activate the current step
            tutorialSteps[currentStepIndex].SetActive(true);
        }
    }

    public void StartGame()
    {
        Debug.Log("Starting the game...");
        SceneManager.LoadScene(gameSceneName);
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
