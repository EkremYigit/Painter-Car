using System.Collections;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform tutorialsGameObjects;
    [SerializeField] private Transform tutorialUICanvasObjects;
    [SerializeField] private Transform NavigationUICanvasObjects;
    [SerializeField] private Transform ScreenMovementButtons;
    [SerializeField] private Transform CountDownUI;
    [SerializeField] private Transform[] mainScreenEnvironments;
    [SerializeField] private Transform[] gameScreenEnvironments;
    [SerializeField] private GameObject LevelCompletedText;
    public static int HighlightedDuration = 3;

    public void OpenTutorial()
    {
        OpenTutorialCanvasElements();
        OpenTutoriGameObjects();
        CloseMainScreenEnvironments();
    }

    public void TabToStart() // if user clicked to game either other buttons this ui function will be called.
    {
        StartNavigation();
    }

    public void StartNavigation()
    {
        CloseMainScreenEnvironments();
        OpenNavigationElements();
    }

    public void OpenNavigationElements()
    {
        NavigationUICanvasObjects.gameObject.SetActive(true);
    }

    public void StartHelper()
    {
        StartCoroutine(Helper());
    }

    IEnumerator Helper()
    {
        NavigationUICanvasObjects.gameObject.SetActive(true);
        GameController.Instance.HighlightSquares();
        GameController.Instance.StopTheCar();
        yield return new WaitForSeconds(HighlightedDuration);
        NavigationUICanvasObjects.gameObject.SetActive(false);
        GameController.Instance.MoveTheCar();
    }

    public void CloseNavigationElements()
    {
        NavigationUICanvasObjects.gameObject.SetActive(false);
        OpenGameEnvironment();
    }

    public void CloseMainScreenEnvironments()
    {
        foreach (var obj in mainScreenEnvironments)
        {
            obj.gameObject.SetActive(false);
        }
    }

    public void OpenMainScreenEnvironments()
    {
        foreach (var obj in mainScreenEnvironments)
        {
            obj.gameObject.SetActive(true);
        }

        CountDownUI.gameObject.SetActive(false);
        ScreenMovementButtons.gameObject.SetActive(false);
    }


    public void CloseTutorial()
    {
        CloseTutorialCanvasElements();
        CloseTutoriGameObjects();
        OpenMainScreenEnvironments();
    }

    private void CloseTutoriGameObjects()
    {
        tutorialsGameObjects.gameObject.SetActive(false);
    }


    private void CloseTutorialCanvasElements()
    {
        tutorialUICanvasObjects.gameObject.SetActive(false);
    }

    private void OpenTutoriGameObjects()
    {
        tutorialsGameObjects.gameObject.SetActive(true);
    }

    private void OpenTutorialCanvasElements()
    {
        tutorialUICanvasObjects.gameObject.SetActive(true);
    }

    public void OpenGameEnvironment()
    {
        foreach (var obj in gameScreenEnvironments)
        {
            obj.gameObject.SetActive(true);
        }

        GameController.Instance.StartTimerAndCarMovement();
    }

    public void ScreenButtonsAvailable()
    {
        ScreenMovementButtons.gameObject.SetActive(true);
    }

    public void ScreenButtonsDisabled() //this function will be called onResumeScreen
    {
        ScreenMovementButtons.gameObject.SetActive(false);
    }

    public void ShowLevelCompletedText()
    {
        LevelCompletedText.SetActive(true);
        StartCoroutine(CloseText());
    }

    IEnumerator CloseText()
    {
        ScreenButtonsDisabled(); //stop movement
        yield return new WaitForSeconds(4f);
        LevelCompletedText.SetActive(false);
        GameController.Instance.EndLevelCompletedAndLoadNextLevel();
    }
}