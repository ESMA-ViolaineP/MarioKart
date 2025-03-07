using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Conditions")]
    [SerializeField] private bool _isACircuitCheckpoint;
    //[SerializeField] private bool _isARespawnCheckpoint;
    [SerializeField] private bool _isAFinishLineCheckpoint;

    [Header("Values")]
    [SerializeField] private int _circuitCheckpointNumber;
    //[SerializeField] private int _respawnCheckpointNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCircuitManager playerCircuitManager = other.GetComponent<PlayerCircuitManager>();

            if (_isACircuitCheckpoint && (playerCircuitManager.CircuitCheckpointCount == _circuitCheckpointNumber - 1))
            {
                playerCircuitManager.CircuitCheckpointCount = _circuitCheckpointNumber;
            }
            //else if (_isARespawnCheckpoint && playerCircuitManager.RespawnCheckpointCount < _circuitCheckpointNumber)
            //{
            //    playerCircuitManager.RespawnCheckpointCount = _respawnCheckpointNumber;
            //}
            else if (_isAFinishLineCheckpoint)
            {
                if (playerCircuitManager.CircuitCheckpointCount == CircuitManager.Instance.TotalCircuitCheckpoints)
                {
                    playerCircuitManager.CircuitCheckpointCount = 0;
                    playerCircuitManager.RespawnCheckpointCount = 0;
                    playerCircuitManager.RoundCount += 1;
                }
                
                if (playerCircuitManager.RoundCount == CircuitManager.Instance.TotalRound)
                {
                    Debug.Log("Circuit terminé ! Bravo !");
                }
            }
        }
    }
}
