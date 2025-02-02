using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public ItemScriptable[] AllItems; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CarController car = other.GetComponent<CarController>();

        if (car != null)
        {
            ItemScriptable randomItem = AllItems[Random.Range(0, AllItems.Length)];
            car.ReceiveItem(randomItem);
            GameObject.Destroy(gameObject);
        }

        
    }
}
