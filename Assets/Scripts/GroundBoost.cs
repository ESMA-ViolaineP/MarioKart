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
            var kartController = other.GetComponent<KartController>();
            kartController.UseBoost = false;
        }
    }
}
