using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private float timeToFinishTheLevel;
    [SerializeField] private SphereController[] sphereControllers;
    [SerializeField] private Vector3 carPosition;
    private int _coloredSphereCount;
    private float _additionalTime;


    private void Awake()
    {
        CountPaintedSpheres();
    }

    public void HighlightSpheres()
    {
        foreach (var sphereController in sphereControllers)
        {
            sphereController.HighlightPaintedSphere();
        }
    }

    private void CountPaintedSpheres()
    {
        foreach (var sphereController in sphereControllers)
        {
            if (sphereController.IsPainted() && !sphereController.IsEnemy())
                _coloredSphereCount++;
        }
    }

    public int GetNumberOfSphereRequiredColor()
    {
        return _coloredSphereCount;
    }

    public float GetTimeToFinishTheLevel()
    {
        return timeToFinishTheLevel;
    }

    public Vector3 GetCarPosition()
    {
        return carPosition;
    }
}