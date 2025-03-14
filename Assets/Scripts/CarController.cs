using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [Header("Acceleration/Deceleration")]
    [SerializeField]
    private float _accelerationFactor, _decelerationFactor, _accelerationLerpInterpolator, _decelerationLerpInterpolator, _rotationSpeed = 0.5f, _speedMaxTurbo = 10;
    private float speed, accelerationSpeed, decelerationSpeed;
    private bool _isAccelerating, _isMovingBackwards;
    
    public bool IsTurbo;
    public float speedMax;

    [SerializeField]
    private LayerMask _layerMask;
    //[SerializeField] private float _terrainSpeedVariator;

    [Header("Curves")]
    [SerializeField]
    private AnimationCurve _accelerationCurve, _decelerationCurve;

    public void Turbo(int ItemDelay)
    {
        if (!IsTurbo)
        {
            StartCoroutine(Turboroutine(ItemDelay));
        }
    }

    private IEnumerator Turboroutine(int ItemDelay)
    {
        IsTurbo = true;
        yield return new WaitForSeconds(ItemDelay);
        IsTurbo = false;
    }

    public void SuperTurbo ()
    {
        if (!IsTurbo)
        {
            IsTurbo = true;
            PlayerCircuitManager.Instance.TimerIsUsed = true;
        }
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

        //if (Physics.Raycast(transform.position, transform.up * -1, out var info,1, _layerMask))
        //{
        //    TerrainComponent terrainBellow = info.transform.GetComponent<TerrainComponent>();
        //    if ( (terrainBellow != null))
        //    {
        //        _terrainSpeedVariator = 1;
        //    }
        //}
    }

    private void FixedUpdate() // FixedUpdate = lie a la Physique
    {

        if (_isAccelerating)
        {
            _accelerationLerpInterpolator += _accelerationFactor;
            _decelerationLerpInterpolator -= _decelerationFactor * 2;
        }
        else
        {
            _decelerationLerpInterpolator += _decelerationFactor;
            _accelerationLerpInterpolator -= _accelerationFactor * 2;
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

        if (IsTurbo)
        {
            speed = _speedMaxTurbo;
        }
        else
        {
            speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * speedMax; //* _terrainSpeedVariator;
        }
    }
}
