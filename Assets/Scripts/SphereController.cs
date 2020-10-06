using DG.Tweening;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField] private Renderer cubeRenderer;
    [SerializeField] private Color sphereColor;
    [SerializeField] private bool isPaintableSphere;
    [SerializeField] private bool isEnemy;

    private Material[] _cubeMaterials;
    private Material _sphereMaterial;

    void OnEnable()
    {
        _cubeMaterials = cubeRenderer.materials;
        _sphereMaterial = GetComponent<Renderer>().material;
    }

    public void HighlightPaintedSphere()
    {
        if (isPaintableSphere)
        {
            _cubeMaterials[1].DOColor(sphereColor, 0.5f)
                .SetLoops((int) (UIController.HighlightedDuration / 0.5) + 2, LoopType.Yoyo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemy && other.CompareTag("Player"))
        {
            GameController.Instance.RestartGame();
        }
        else if (isPaintableSphere && other.CompareTag("Player"))
        {
            GameController.Instance.CubeIsColored();
            _sphereMaterial.color = sphereColor;
            _cubeMaterials[1].color = sphereColor;
            Destroy(gameObject);
        }
    }

    public bool IsPainted()
    {
        return isPaintableSphere;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }
}