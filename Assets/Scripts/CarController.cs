using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    public float speedMax;

    [Header("Acceleration/Deceleration")]
    [SerializeField]
    private float _accelerationFactor, _decelerationFactor, _accelerationLerpInterpolator, _decelerationLerpInterpolator, _rotationSpeed = 0.5f;
    private float speed, accelerationSpeed, decelerationSpeed;
    private bool _isAccelerating, _isMovingBackwards;

    [SerializeField]
    private LayerMask _layerMask;
    //[SerializeField] private float _terrainSpeedVariator;


    [Header("Curves")]
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

        //if (Physics.Raycast(transform.position, transform.up * -1, out var info,1, _layerMask))
        //{
        //    TerrainComponent terrainBellow = info.transform.GetComponent<TerrainComponent>();
        //    if ( (terrainBellow != null))
        //    {
        //        _terrainSpeedVariator = 1;
        //    }
        //}

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
    }

    public void ReceiveItem(ItemScriptable randomItem)
    {
        GameManager.Instance.CurrentItem = randomItem;
    }
    
    private void UseItem()
    {
        if (GameManager.Instance.CurrentItem.ItemType == ItemType.None)
        {
            return;
        }

        switch (GameManager.Instance.CurrentItem.ItemType)
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

        GameManager.Instance.ImageSlot001.sprite = null;
    }

    private void UseSpeedBoost()
    {
        if (GameManager.Instance.CurrentItem.ItemName == "Red Mushroom")
        {
            StartCoroutine(SpeedBoost(1, 1));
        }
        else if (GameManager.Instance.CurrentItem.ItemName == "Star")
        {
            StartCoroutine(SpeedBoost(5, 5));
        }
    }
    private IEnumerator SpeedBoost( float boostValue, float boostDuration)
    {
        float originalSpeedValue = speedMax;
        speedMax = boostValue;
        yield return new WaitForSeconds(boostDuration);
        speedMax = originalSpeedValue;
    }

    private void FireMissile()
    {

    }

    private void DropTrap()
    {

    }

}
