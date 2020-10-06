using DG.Tweening;
using UnityEngine;

public class ScaleIncreaseDecrease : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 destinationScaleVal;
    [SerializeField] private float time;
    [SerializeField] private RectTransform uiTransform;

    void Start()
    {
        uiTransform.DOScale(destinationScaleVal, time).SetLoops(-1, LoopType.Yoyo);
    }
}