using UnityEngine;

public class ItemBananaBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        KartController kartController = other.GetComponent<KartController>();
        if (kartController != null)
        {
            kartController.Trap(0.5f);
            Destroy(gameObject);
        }
    }
}
