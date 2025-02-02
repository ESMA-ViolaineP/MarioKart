using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    public float speedMax;

    [SerializeField]
    private float _accelerationFactor, _decelerationFactor, _accelerationLerpInterpolator, _decelerationLerpInterpolator, _rotationSpeed = 0.5f;
    [SerializeField]
    private float _boostDuration, _boostValue;
    private float speed, accelerationSpeed, decelerationSpeed;
    private bool _isAccelerating, _isMovingBackwards;

    public ItemScriptable currentItem;

    [SerializeField]
    private AnimationCurve _accelerationCurve, _decelerationCurve;

    void Start()
    {

    }

    void Update()
    {
        // Avancer
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isAccelerating = true;
            _isMovingBackwards = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _isAccelerating = false;
        }

        // Reculer
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

        // Tourner à gauche
        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles += Vector3.down * _rotationSpeed * Time.deltaTime; 
        }

        // Tourner à droite
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime;
        }

        // Utiliser un Item
        if (Input.GetKey(KeyCode.E))
        {
            UseItem();
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

    public void ReceiveItem(ItemScriptable randomItem)
    {
        currentItem = randomItem;
    }
    private void UseItem()
    {
        if (currentItem.ItemType == ItemType.None)
        {
            return;
        }

        switch (currentItem.ItemType)
        {
            case ItemType.SpeedBoost:
                UseSpeedBoost();
                break;
            case ItemType.Missile:
                FireMissile();
                break;
            case ItemType.Trap:
                DropTrap();
                break;
        }
    }

    private void UseSpeedBoost()
    {
        if (currentItem.ItemName == "Red Mushroom")
        {
            StartCoroutine(SpeedBoost());
        }
    }
    private IEnumerator SpeedBoost()
    {
        float originalSpeedValue = speedMax;
        speedMax = _boostValue;
        yield return new WaitForSeconds(_boostDuration);
        speedMax = originalSpeedValue;
    }

    private void FireMissile()
    {

    }

    private void DropTrap()
    {

    }

}
