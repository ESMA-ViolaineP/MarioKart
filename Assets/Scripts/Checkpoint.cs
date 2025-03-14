using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Conditions")]
    [SerializeField] private bool _isACircuitCheckpoint;
    [SerializeField] private bool _isAFinishLineCheckpoint;

    [Header("Values")]
    [SerializeField] private int _circuitCheckpointNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCircuitManager.Instance.CheckpointPosition = other.GetComponent<Transform>().position;

            if (_isACircuitCheckpoint && (PlayerCircuitManager.Instance.CircuitCheckpointCount == _circuitCheckpointNumber - 1))
            {
                PlayerCircuitManager.Instance.CircuitCheckpointCount = _circuitCheckpointNumber;
            }

            else if (_isAFinishLineCheckpoint)
            {
                if (PlayerCircuitManager.Instance.CircuitCheckpointCount == PlayerCircuitManager.Instance.TotalCircuitCheckpoints)
                {
                    PlayerCircuitManager.Instance.CircuitCheckpointCount = 0;
                    PlayerCircuitManager.Instance.RoundCount += 1;
                }
                
                if (PlayerCircuitManager.Instance.RoundCount == PlayerCircuitManager.Instance.TotalRound)
                {
                    Debug.Log("Circuit termine ! Bravo !");
                }
            }
        }
    }
}
