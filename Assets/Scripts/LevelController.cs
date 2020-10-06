using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject[] levels; //Contains LevelItemScripts To Determine Level Properties.
    [SerializeField] private HintMap hintMap;
    [SerializeField] private ProgressBarController progressBarController;
    [SerializeField] private CountDownTimerController countDownTimerController;
    [SerializeField] private Transform taxiPosition;
    private BoardController _boardController;
    private GameObject _currentLevel;
    private int _progressCount;
    private int _requiredProgress;
    private static int _index; // it refers current level that represents to user as a game map.


    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        LoadMapArea();
        _boardController = FindObjectOfType<BoardController>();
        ResetCarPosition();
        GetRequiredProgress();
        LoadProgressBar();
        LoadCountdownTimer();
    }

    private void GetRequiredProgress()
    {
        _requiredProgress = _boardController.GetNumberOfSphereRequiredColor();
    }

    private void ResetCarPosition()
    {
        taxiPosition.position = _boardController.GetCarPosition();
        taxiPosition.rotation = Quaternion.identity;
    }


    public void HighlightSquares()
    {
        hintMap.ReminderButtonOnClick();
        _boardController.HighlightSpheres();
    }


    public void LoadNextLevel()
    {
        DestroyLevel();
        IncreaseLevelIndex();
        LoadLevel();
    }

    private void LoadMapArea()
    {
        _currentLevel = Instantiate(levels[_index]);
    }

    public void RestartLevel()
    {
        countDownTimerController.ResetTimer();
        DestroyLevel();
        LoadLevel();
    }

    private void IncreaseLevelIndex()
    {
        if (_index < levels.Length - 1)
            _index++;
    }

    private void DecreaseIndex()
    {
        if (_index > 0 && _index != levels.Length - 1)
            _index--;
    }

    private void DestroyLevel()
    {
        if (_currentLevel)
            Destroy(_currentLevel);
    }


    public void IncreaseLevelProgress()
    {
        _progressCount++;
        progressBarController.IncreaseProgress();
        if (IsLevelCompleted())
        {
            countDownTimerController.ChangeTimerState(); //stop timer.
            LevelIsCompleted();
        }
    }

    private bool IsLevelCompleted()
    {
        return _progressCount == _requiredProgress;
    }

    private void LevelIsCompleted()
    {
        GameController.Instance.LevelCompleted();
    }

    private void LoadProgressBar()
    {
        //clear all current level properties before next level load
        ResetCurrentLevelPropertiesForNextLevel();

        progressBarController.RequiredProgress = _boardController.GetNumberOfSphereRequiredColor();
        progressBarController.LoadLevelText(_index);
    }


    private void LoadCountdownTimer()
    {
        countDownTimerController.TimeLeft = _boardController.GetTimeToFinishTheLevel();
        countDownTimerController.LoadTimer();
        // countDownTimerController.StartCountDownTimer();
    }


    public void StartOrStopTimer()
    {
        countDownTimerController
            .ChangeTimerState(); //this function changes the timer state if timer already started this function stop the timer.
    }

    private void ResetCurrentLevelPropertiesForNextLevel()
    {
        //progress bar will be cleared and level controller level properties will be reset
        _progressCount = 0;
        progressBarController.ClearProgressbarProperties();
    }

    public void LoadCurrentLevelProperties()
    {
        //after next level load called this function will update next properties for next level to progress bar
    }

    public void StartTimer()
    {
        countDownTimerController.StartCountDownTimer();
    }
}