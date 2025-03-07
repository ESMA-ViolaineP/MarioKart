using UnityEngine;

public class RaycastDetecting : MonoBehaviour
{
    [SerializeField]
    private float _raycastDistance;

    [SerializeField]
    private LayerMask _layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.I))
        {
            if(Physics.Raycast(transform.position, transform.forward, out var info, _raycastDistance, _layerMask))
            {
                print("Coucou");
            }
        }
    }
}
