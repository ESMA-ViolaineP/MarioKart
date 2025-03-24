using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathZone : MonoBehaviour
{
    private bool isTrigerred;
    [SerializeField] private GameObject _transitionImage;

    void OnTriggerEnter(Collider other)
    {
        if (isTrigerred == false)
        {
            //other.GetComponent<Transform>().position = PlayerCircuitManager.Instance.CheckpointPosition;
        }

        isTrigerred = true;
    }

    void OnTriggerExit(Collider other)
    {
        isTrigerred = false;
    }
}
