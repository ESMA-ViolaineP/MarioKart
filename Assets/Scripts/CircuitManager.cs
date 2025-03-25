using TMPro;
using UnityEngine;

public class CircuitManager : MonoBehaviour
{
    [Header("Player Informations")]
    [Header("Player Condition")]
    public bool WinPlayer001;
    public bool WinPlayer002;

    [Header("Player Position Values")]
    public int CheckpointsNumberPlayer001;
    public int CheckpointsNumberPlayer002;
    public int LapNumberPlayer001, LapNumberPlayer002;
    public float DistancePlayer001, DistancePlayer002;

    [Header("Player Position UI")]
    [SerializeField]
    private GameObject _positionOnePlayer01;
    [SerializeField]
    private GameObject _positionTwoPlayer01;
    [SerializeField]
    private GameObject _positionOnePlayer02;
    [SerializeField]
    private GameObject _positionTwoPlayer02;

    [Header("Menu")]
    [Header("Menu To Activate")]
    [SerializeField]
    private GameObject _resultMenu;

    void Start()
    {
     _positionOnePlayer01.SetActive(true);   
     _positionTwoPlayer02.SetActive(true);   
    }

    void Update()
    {
        if (WinPlayer001 && WinPlayer002)
        {
            _resultMenu.SetActive(true);

            gameObject.SetActive(false);
        }

        if (LapNumberPlayer001 > LapNumberPlayer002)
        {
            _positionOnePlayer01.SetActive(true);
            _positionTwoPlayer01.SetActive(false);
            _positionOnePlayer02.SetActive(false);
            _positionTwoPlayer02.SetActive(true);
        }
        else if (LapNumberPlayer001 < LapNumberPlayer002)
        {
            _positionOnePlayer01.SetActive(false);
            _positionTwoPlayer01.SetActive(true);
            _positionOnePlayer02.SetActive(true);
            _positionTwoPlayer02.SetActive(false);
        }
        else if (LapNumberPlayer001 == LapNumberPlayer002)
        {
            if (CheckpointsNumberPlayer001 > CheckpointsNumberPlayer002)
            {
                _positionOnePlayer01.SetActive(true);
                _positionTwoPlayer01.SetActive(false);
                _positionOnePlayer02.SetActive(false);
                _positionTwoPlayer02.SetActive(true);
            }
            else if (CheckpointsNumberPlayer001 < CheckpointsNumberPlayer002)
            {
                _positionOnePlayer01.SetActive(false);
                _positionTwoPlayer01.SetActive(true);
                _positionOnePlayer02.SetActive(true);
                _positionTwoPlayer02.SetActive(false);
            }
            else if (CheckpointsNumberPlayer001 == CheckpointsNumberPlayer002)
            {
                if (DistancePlayer001 > DistancePlayer002)
                {
                    _positionOnePlayer01.SetActive(true);
                    _positionTwoPlayer01.SetActive(false);
                    _positionOnePlayer02.SetActive(false);
                    _positionTwoPlayer02.SetActive(true);
                }

                else if (DistancePlayer001 < DistancePlayer002)
                {
                    _positionOnePlayer01.SetActive(false);
                    _positionTwoPlayer01.SetActive(true);
                    _positionOnePlayer02.SetActive(true);
                    _positionTwoPlayer02.SetActive(false);
                }
            }
        }
    }
}
