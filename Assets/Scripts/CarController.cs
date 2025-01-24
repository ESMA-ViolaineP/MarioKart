using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _rotationSpeed = 0.5f, _speed;

    [SerializeField]
    private float _minSpeed, _maxSpeed;

    void Start()
    {
        _minSpeed = -25;
        _maxSpeed = 25;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles += Vector3.down*_rotationSpeed * Time.deltaTime; 
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles += Vector3.up* _rotationSpeed * Time.deltaTime;
        }

        var xAngle = Mathf.Clamp(transform.eulerAngles.x + 360, 320, 400);
        var yAngle = transform.eulerAngles.y;
        var zAngle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
    }

    private void FixedUpdate() // FixedUpdate = lié à la Physique
    {
        if (Input.GetKey(KeyCode.Space))
        {
           _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
        }  
    }
}
