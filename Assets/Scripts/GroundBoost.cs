using System.Collections;
using UnityEngine;

public class GroundBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var kartController = other.GetComponent<KartController>();
            kartController.UseBoost = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TurboRoutineEnd(other));
        }
    }

    private IEnumerator TurboRoutineEnd(Collider other)
    {
        yield return new WaitForSeconds(1);
        var kartController = other.GetComponent<KartController>();
        kartController.UseBoost = false;
    }

}
