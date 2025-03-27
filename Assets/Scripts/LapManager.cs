using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LapManager : MonoBehaviour
{
    [Header("Checkpoint")]
    [SerializeField]
    private List<CheckPoint> _checkpoints;
    private CheckPoint lastCheckpoint;
    private int numberOfCheckpoints, checkpointCount;
    private float distanceCheckpoint;
    public Vector3 PositionLastCheckpoint;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _lapText;
    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private bool _isPlayerOne;
    private int lapNumber;

    void Start()
    {
        _lapText.text = "Tour : " + lapNumber + "/3";

        numberOfCheckpoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None).Length;
        _checkpoints = new List<CheckPoint>();
    }

    void Update()
    {
        if (_checkpoints.Count > 0)
        {
            lastCheckpoint = _checkpoints[_checkpoints.Count - 1];
        }

        if (lastCheckpoint != null)
        {
            PositionLastCheckpoint = lastCheckpoint.transform.position;
            distanceCheckpoint = Vector3.Distance(transform.position, PositionLastCheckpoint);

            if (_isPlayerOne)
            {
                GameManager.Instance.CircuitManager.DistancePlayer001 = distanceCheckpoint;
            }
            else
            {
                GameManager.Instance.CircuitManager.DistancePlayer002 = distanceCheckpoint;
            }
        }
    }

    public void AddCheckPoint(CheckPoint checkPointToAdd)
    {
        if (checkPointToAdd.IsFinishLine)
        {
            FinishLap();
        }

        if (_checkpoints.Contains(checkPointToAdd) == false)
        {
            _checkpoints.Add(checkPointToAdd);
            checkpointCount++;

            if (_isPlayerOne)
            {
                GameManager.Instance.CircuitManager.CheckpointsNumberPlayer001 = checkpointCount;
            }
            else
            {
                GameManager.Instance.CircuitManager.CheckpointsNumberPlayer002 = checkpointCount;
            }
        }
    }

    private void FinishLap()
    {
        if (_checkpoints.Count > numberOfCheckpoints / 2)
        {
            lapNumber++;

            if (_isPlayerOne)
            {
                GameManager.Instance.CircuitManager.LapNumberPlayer001 = lapNumber;
            }
            else
            {
                GameManager.Instance.CircuitManager.LapNumberPlayer002 = lapNumber;
            }

            _lapText.text = "Tour : " + lapNumber + "/3";
            _checkpoints.Clear();

            if (lapNumber >= 3)
            {
                if (_isPlayerOne)
                {
                    GameManager.Instance.CircuitManager.WinPlayer001 = true;
                }
                else
                {
                    GameManager.Instance.CircuitManager.WinPlayer002 = true;
                }
                _menu.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}

