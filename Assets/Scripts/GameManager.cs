using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Material[] ColorMaterialKart;

    public int SelectionPlayer01;
    public int SelectionPlayer02;
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
}
