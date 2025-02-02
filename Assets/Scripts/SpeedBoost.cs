using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private CarController carBehaviour;

    private float originalSpeedValue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        carBehaviour = other.GetComponent<CarController>();
        originalSpeedValue = carBehaviour.speedMax;
        carBehaviour.speedMax = originalSpeedValue;
    }

    private void OnTriggerExit(Collider other)
    {
        carBehaviour.speedMax = originalSpeedValue;
    }
}
