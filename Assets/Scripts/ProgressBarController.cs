using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Image frontImage;

    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI nextLevel;

    private int _requiredProgress;

    private float _progressCounter;

    public int RequiredProgress
    {
        get => _requiredProgress;
        set => _requiredProgress = value;
    }

    public void ClearProgressbarProperties()
    {
        _progressCounter = 0;
        frontImage.fillAmount = 0;
    }

    public void IncreaseProgress()
    {
        _progressCounter++;
        frontImage.DOFillAmount(_progressCounter / (RequiredProgress), 1f);
    }

    public void LoadLevelText(int index)
    {
        currentLevel.SetText("" + (index + 1));
        nextLevel.SetText("" + (index + 2));
    }
}