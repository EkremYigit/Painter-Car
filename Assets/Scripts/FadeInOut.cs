using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private Image objectColor;
    [SerializeField] private float time;

    private void Start()
    {
        objectColor.DOFade(1, time).From(0).SetLoops(-1, LoopType.Restart);
    }
}