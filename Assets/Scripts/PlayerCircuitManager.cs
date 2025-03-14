using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCircuitManager : MonoBehaviour
{
    public static PlayerCircuitManager Instance;

    [Header("Checkpoint & Round")]
    [SerializeField] private TextMeshProUGUI _roundCountText;
    public int CircuitCheckpointCount, RoundCount, TotalRound, TotalCircuitCheckpoints;
    public Vector3 CheckpointPosition;


    [Header("Item Timer")]
    [SerializeField] private Image _timerImage;
    public bool TimerIsUsed;
    private float currentAmount, speed;

    public CarController PlayerCarController;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CheckpointPosition = new Vector3(45.5f, 0.621f, -16.3f);
    }

    void Update()
    {
        _roundCountText.text = "Round : " + RoundCount + "/" + PlayerCircuitManager.Instance.TotalRound;
    
        if (TimerIsUsed)
        {
            if (currentAmount < 100)
            {
                currentAmount += speed * Time.deltaTime;
                _timerImage.fillAmount = currentAmount / 100;
            }
            if (currentAmount >= 100)
            {
                PlayerCarController.IsTurbo = false;
            }
        }
    }
}
