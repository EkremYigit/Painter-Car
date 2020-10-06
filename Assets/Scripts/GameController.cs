using System.Runtime.CompilerServices;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public LevelController levelController;
    public MoveController carController;
    public UIController uiController;
    public GameObject partSystem;
    public VibrationController vibrationController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CubeIsColored()
    {
        // this function is triggered from sphere trigger collider when the car is collided with sphere.
        levelController.IncreaseLevelProgress();
        //TODO -> Get boolean value from settings 
        // if (isVibration)
        vibrationController.VibratePhone();
    }

    public void LevelCompleted()
    {
        //This is triggered from LevelController when the level is completed successfully;
        carController.ChangeState(); //its stop the car.
        ActivateCompleteParticleSystem();
        uiController.ShowLevelCompletedText();
    }

    private void ActivateCompleteParticleSystem()
    {
        partSystem.SetActive(true);
    }

    private void DeActiveParticle()
    {
        partSystem.SetActive(false);
    }

    public void StartNavigation() //When user tap to start game. this function is called.
    {
        //After navigation end start the time and set active game play buttons.
        uiController.StartNavigation();
        levelController.HighlightSquares();
        //after Highlight navigation close is called. and GameEnvironments are called from ui controller.
    }

    public void CloseNavigation()
    {
        uiController.CloseNavigationElements();
    }

    public void StartTimerAndCarMovement()
    {
        levelController.StartTimer();
        carController.MoveCar();
        uiController.ScreenButtonsAvailable();
    }

    public void MoveTheCar()
    {
        carController.MoveCar();
    }

    public void StopTheCar()
    {
        carController.ChangeState();
    }

    public void HighlightSquares()
    {
        levelController.HighlightSquares();
    }


    private void LoadNextLevel()
    {
        levelController.LoadNextLevel();
        uiController.OpenMainScreenEnvironments();
        gameObject.GetComponent<TabToStart>().isStarted = false;
    }

    public void EndLevelCompletedAndLoadNextLevel()
    {
        DeActiveParticle(); //until load next level, completed text and particle system are closed before load.
        LoadNextLevel();
    }


    public void RestartGame()
    {
        //Load Current Level Open The UI Menu Again
        levelController.RestartLevel();
        carController.ResetCar();
        uiController.OpenMainScreenEnvironments();
        gameObject.GetComponent<TabToStart>().isStarted = false;
    }
}