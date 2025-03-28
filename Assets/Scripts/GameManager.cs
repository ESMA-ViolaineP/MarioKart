using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Script")]
    public CircuitManager CircuitManager;

    [Header("Lists")]
    public Material[] ColorMaterialKart;
    public Sprite[] ImageKart;

    [Header("Players Informations")]
    [Header("Player selection")]
    public int SelectionPlayer01;
    public int SelectionPlayer02;

    [Header("Player Name")]
    public string NamePlayer01;
    public string NamePlayer02;

    [Header("Player Points")]
    public int PointsPlayer01;
    public int PointsPlayer02;


    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FindCircuitManager(CircuitManager circuitManager)
    {
        CircuitManager = circuitManager;
    }
}
