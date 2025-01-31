using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _accelerationFactor, _accelerationLerpInterpolator, _rotationSpeed = 0.5f;

    public float speedMax = 3;

    [SerializeField]
    private float _deccelerationFactor, _deccelerationLerpInterpolator;

    private float speed;
    private bool _isAccelerating;

    private bool isMovingFoward;

    [SerializeField]
    private AnimationCurve _accelerationCurve;
    [SerializeField]//test
    private AnimationCurve _deccelerationCurve;

    void Start()
    {

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    isMovingFoward = false;
        //    _isAccelerating = true;
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    _isAccelerating = true;
        //}

        if (Input.GetKeyDown(KeyCode.W))
        {
            //isMovingFoward = true;
            _isAccelerating = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _isAccelerating = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += Vector3.down * 
                _rotationSpeed * Time.deltaTime; 
        }

        if (Input.GetKey(KeyCode.RightArrow))
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
            _accelerationLerpInterpolator -= _deccelerationFactor * 2;

            speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * speedMax;

        }
        else
        {
            _accelerationLerpInterpolator -= _accelerationFactor * 2;
            _deccelerationLerpInterpolator += _deccelerationFactor;

            speed = _deccelerationCurve.Evaluate(_deccelerationLerpInterpolator) * speedMax;
        }
        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator);
        _deccelerationLerpInterpolator = 1 - Mathf.Clamp01(_accelerationLerpInterpolator);

        _rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);



        //if (isMovingFoward)
        //{
        //    _rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    _rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
        //}
    }


}
