using System.Collections;
using UnityEngine;

public class EmptyZone : MonoBehaviour
{
    private bool playerStillInside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("SlowEmptyZone"))
            {
                playerStillInside = true;
                StartCoroutine(CheckIfStillInside(other));
            }
            else
            {
                StartCoroutine(Respawn(other));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStillInside = false;
        }
    }

    private IEnumerator CheckIfStillInside(Collider other)
    {
        yield return new WaitForSeconds(1.5f);

        if (playerStillInside)
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
