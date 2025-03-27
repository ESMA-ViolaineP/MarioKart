using System.Collections;
using UnityEngine;

public class EmptyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("SlowEmptyZone"))
            {
                StartCoroutine(CheckIfStillInside(other));
            }
            else
            {
                StartCoroutine(Respawn(other));
            }
        }
    }

    private IEnumerator CheckIfStillInside(Collider other)
    {
        yield return new WaitForSeconds(1);

        if (other != null && other.CompareTag("Player"))
        {
            StartCoroutine(Respawn(other));
        }
    }

    private IEnumerator Respawn(Collider other)
    {
        var kartController = other.GetComponent<KartController>();
        kartController.IsAccelerating = false;
        kartController.AccelerationLerpInterpolator = 0;
        kartController.Speed = 0;
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        var lapManager = other.GetComponent<LapManager>();
        other.transform.position = lapManager.PositionLastCheckpoint;
        other.gameObject.SetActive(true);
    }
}
