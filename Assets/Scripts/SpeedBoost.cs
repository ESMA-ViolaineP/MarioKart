using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private float speedBoost = 9;

    private CarController carBehaviour;
    private float speedOriginal;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        carBehaviour = other.GetComponent<CarController>();
        speedOriginal = carBehaviour.speedMax;
        carBehaviour.speedMax = speedBoost;
    }

    private void OnTriggerExit(Collider other)
    {
        // Utiliser Decceleration
    }
}
