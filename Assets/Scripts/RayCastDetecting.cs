using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDetecting : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;

    [SerializeField] private LayerMask _layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Physics.Raycast(transform.position, transform.up*-1, out var info, _raycastDistance, _layerMask))
            {
                Debug.Log("Touché");
            }
        }
        // To debug Raycast : Debug.DrawRay(transform.position, transform.forward * _raycastDistance);
    }
}
