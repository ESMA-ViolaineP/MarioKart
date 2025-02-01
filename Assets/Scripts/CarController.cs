using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _accelerationFactor, _decelerationFactor, _accelerationLerpInterpolator, _decelerationLerpInterpolator, _rotationSpeed = 0.5f;

    public float speedMax;

    private float speed;
    private float accelerationSpeed, decelerationSpeed;
    private bool _isAccelerating, _isMovingBackwards;

    [SerializeField]
    private AnimationCurve _accelerationCurve, _decelerationCurve;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            _isAccelerating = true;
            _isMovingBackwards = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _isAccelerating = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _isAccelerating = true;
            _isMovingBackwards = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            _isAccelerating = false;
            _isMovingBackwards = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += Vector3.down * _rotationSpeed * Time.deltaTime; 
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime;
        }

        var xAngle = Mathf.Clamp(transform.eulerAngles.x + 360, 320, 400);
        var yAngle = transform.eulerAngles.y;
        var zAngle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
    }

    private void FixedUpdate() // FixedUpdate = lié à la Physique
    {
        if (_isAccelerating)
        {
            _accelerationLerpInterpolator += _accelerationFactor;
            _decelerationLerpInterpolator -= _decelerationFactor * 2;
        }
        else
        {
            _accelerationLerpInterpolator -= _accelerationFactor * 2;
            _decelerationLerpInterpolator += _decelerationFactor;
        }


        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator);
        _decelerationLerpInterpolator = Mathf.Clamp01(_decelerationLerpInterpolator);

        accelerationSpeed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * speedMax;
        decelerationSpeed = _decelerationCurve.Evaluate(_decelerationLerpInterpolator) * speedMax;

        speed = _isAccelerating ? accelerationSpeed : decelerationSpeed;

        if (_isMovingBackwards)
        {
            _rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);

        }
        else
        {
            _rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }
}
