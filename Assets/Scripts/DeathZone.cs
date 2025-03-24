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
            StartCoroutine(transition(other));
        }

        isTrigerred = true;
    }

    void OnTriggerExit(Collider other)
    {
        isTrigerred = false;
    }

    private IEnumerator transition(Collider other)
    {
        _transitionImage.SetActive(true);
        yield return new WaitForSeconds(1);
        //other.GetComponent<Transform>().position = PlayerCircuitManager.Instance.CheckpointPosition;
        yield return new WaitForSeconds(0.5f);
        _transitionImage.SetActive(false);
    }
}
