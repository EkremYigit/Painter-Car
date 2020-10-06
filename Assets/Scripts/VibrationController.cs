using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    public void VibratePhone()
    {
        Handheld.Vibrate();
    }
}