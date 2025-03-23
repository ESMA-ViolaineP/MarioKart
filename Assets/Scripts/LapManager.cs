using System.Collections.Generic;
using UnityEngine;


public class LapManager : MonoBehaviour
{
    private int _lapNumber;
    private List<CheckPoint> _checkpoints;
    private int _numberOfCheckpoints;


    private void Start()
    {
        _numberOfCheckpoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None).Length;
        _checkpoints = new List<CheckPoint>();
    }

    public void AddCheckPoint(CheckPoint checkPointToAdd)
    {
        if(checkPointToAdd.isFinishLine)
        {
            FinishLap();
        }

        if(_checkpoints.Contains(checkPointToAdd) == false)
        {
            _checkpoints.Add(checkPointToAdd);
        }
    }

    private void FinishLap()
    {
        if (_checkpoints.Count > _numberOfCheckpoints/2)
        {
            _lapNumber++;
            Debug.Log("Tour Fini, on entre dans le tour " +  _lapNumber);
            _checkpoints.Clear();
            if(_lapNumber>=3)
            {
                Debug.Log("Gg WP");
            }
        }
    }
}
