using System;
using DG.Tweening;
using UnityEngine;


public class MoveController : MonoBehaviour
{
    [Tooltip("Attached object moves with keyboard arrows")] [SerializeField]
    private Transform attachedObjectToMove;

    [Tooltip("Motor Force")] [SerializeField]
    private float motorForce;

    [Tooltip("The power required to set the vehicle in motion")] [SerializeField]
    private float breakForce;

    [Tooltip("The Time that passes during rotating car ")] [Range(0.1f, 2)] [SerializeField]
    private float rotationTime;

    [SerializeField] private WheelCollider LF_Collider;
    [SerializeField] private WheelCollider RF_Collider;
    [SerializeField] private WheelCollider LB_Collider;
    [SerializeField] private WheelCollider RB_Collider;

    [SerializeField] private Transform LF_Transform;
    [SerializeField] private Transform RF_Transform;
    [SerializeField] private Transform LB_Transform;
    [SerializeField] private Transform RB_Transform;


    private float verticalInput;
    private float horizontalInput;

    private bool _isRotating;
    private float _currentBreakForce;
    private bool _isBreaking;

    // Timer Multiplier 

    void Start()
    {
        _isBreaking = true;
    }


    void FixedUpdate()
    {
        MoveFront();
        HandleMotor();
        UpdateWheels();
    }

    public void ChangeState() //this stop the car.
    {
        _isBreaking = !_isBreaking;
    }

    private void MoveFront()
    {
        verticalInput = 1;
    }


    private void HandleMotor() // Motorlardan tekerlere guc aktariliyormus gibi davranıyor bu fonksiyon.
    {
        _currentBreakForce = _isBreaking ? breakForce : 0f;
        if (_isBreaking) //isBreaking return true while rotating for this car stops.
        {
            ApplyBreaking();
        }

        ApplyMotorForceToWheels();
    }

    private void ApplyMotorForceToWheels()
    {
        LF_Collider.motorTorque = verticalInput * motorForce;
        RF_Collider.motorTorque = verticalInput * motorForce;
        RB_Collider.motorTorque = verticalInput * motorForce;
        LB_Collider.motorTorque = verticalInput * motorForce;
    }

    private void ApplyBreaking() // stop car 
    {
        LF_Collider.brakeTorque = _currentBreakForce;
        LB_Collider.brakeTorque = _currentBreakForce;
        RF_Collider.brakeTorque = _currentBreakForce;
        RB_Collider.brakeTorque = _currentBreakForce;
    }

    private void UpdateWheels()
    {
        RotateGivenWheel(LF_Collider, LF_Transform);
        RotateGivenWheel(RF_Collider, RF_Transform);
        RotateGivenWheel(LB_Collider, LB_Transform);
        RotateGivenWheel(RB_Collider, RB_Transform);
    }

    private void RotateGivenWheel(WheelCollider lfCollider, Transform lfTransform)
    {
        Vector3 pos;
        Quaternion rot;
        lfCollider.GetWorldPose(out pos, out rot);
        lfTransform.rotation = rot;
        lfTransform.position = pos;
    }


    public void RotateLeft()
    {
        if (_isRotating) return;
        _isBreaking = true;
        _isRotating = true;
        attachedObjectToMove.DORotate(new Vector3(0, attachedObjectToMove.rotation.eulerAngles.y - 90, 0), rotationTime)
            .OnComplete(() =>
            {
                _isRotating = false;
                ResetBreaking();
            });
    }

    public void RotateRight()
    {
        if (_isRotating) return;
        _isBreaking = true;
        _isRotating = true;
        //Debug.Log("Local Rotation : " + attachedObjectToMove.rotation.eulerAngles.y);
        attachedObjectToMove.DORotate(new Vector3(0, attachedObjectToMove.rotation.eulerAngles.y + 90, 0), rotationTime)
            .OnComplete(() =>
            {
                _isRotating = false;
                ResetBreaking(); //this function need to accelerate after rotating. Cause I used to 1k break torque to stop vehicle.
            });
    }


    private void ResetBreaking()
    {
        _isBreaking = false;
        _currentBreakForce = 0;
        ApplyBreaking();
    }

    public void MoveCar()
    {
        ResetBreaking();
    }

    public void ResetCar()
    {
        ChangeState();
    }
}