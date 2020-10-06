using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimerController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Image frontCircle;
    [SerializeField] private RectTransform arrowImage;
    [SerializeField] private TextMeshProUGUI secondText;

    private Tween _tweenOfCircleCalculate;
    private Tween _tweenOfArrowRotation;

    private float _tweenTime;
    private bool _isTimeStopped;
    private float _timeLeft;


    public float TimeLeft
    {
        set => _timeLeft = value;
    }

    void Update()
    {
        if (!_isTimeStopped)
        {
            _timeLeft -= Time.deltaTime;
            secondText.text = (_timeLeft).ToString("0");
            if (_timeLeft <= 0)
            {
                gameController.RestartGame();
            }
        }
    }

    public void StartCountDownTimer()
    {
        _isTimeStopped = false;
        _tweenTime = _timeLeft;
        StartArrowRotation();
        StartFrontCircle();
    }

    private void StartFrontCircle()
    {
        //Calculate the ratio of remaining time to 1-0 
        _tweenOfCircleCalculate = frontCircle.DOFillAmount(0, _tweenTime).From(1)
            .SetEase(Ease.Linear)
            .SetRelative();
    }

    private void StartArrowRotation()
    {
        //Calculate the ratio of remaining time to 360 degrees
        _tweenOfArrowRotation = arrowImage.DORotate(new Vector3(0, 0, -360f), _tweenTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetRelative();
    }

    public void ChangeTimerState()
    {
        _isTimeStopped = !_isTimeStopped;
        _tweenOfCircleCalculate.TogglePause();
        _tweenOfArrowRotation.TogglePause(); //???
    }

    public void LoadTimer()
    {
        _isTimeStopped = false;
        _tweenTime = _timeLeft;
        StartArrowRotation();
        StartFrontCircle();
        ChangeTimerState();
        arrowImage.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void ResetTimer()
    {
        _tweenOfArrowRotation.Restart();
        _tweenOfCircleCalculate.Restart();
        ChangeTimerState();
    }
}