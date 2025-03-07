using UnityEngine;
using TMPro;

public class PlayerCircuitManager : MonoBehaviour
{
    public int CircuitCheckpointCount; 
    public int RespawnCheckpointCount;
    public int RoundCount;

    [SerializeField] private TextMeshProUGUI _roundCountText;

    void Start()
    {

    }

    void Update()
    {
        _roundCountText.text = "Round : " + RoundCount + "/" + CircuitManager.Instance.TotalRound;
    }
}
