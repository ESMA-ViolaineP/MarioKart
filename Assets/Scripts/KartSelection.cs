using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KartSelection : MonoBehaviour
{
    [Header("Karts")]
    [SerializeField]
    private GameObject[] _karts;
    [SerializeField]
    private Transform _kartTransform;

    private GameObject currentKart;
    private int currentIndex = 0;

    [Header("Player")]
    [SerializeField]
    private bool _isPlayerOne;
    private int indexPlayers;
    private Color customColor;

    [Header("UI Menus")]
    [SerializeField]
    private Button[] _buttons;
    [SerializeField]
    private Image[] _buttonsOutliners;
    [SerializeField]
    private GameObject _playerChoosingText001;
    [SerializeField]
    private GameObject _playerChoosingText002;

    [SerializeField]
    private GameObject _startMenu;
    [SerializeField]
    private GameObject _selectionMenu;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt("Kart_Player_001", 0);
        currentIndex = PlayerPrefs.GetInt("Kart_Player_002", 1);
        SpawnKart(currentIndex);
    }

    private void Update()
    {
        if (currentKart != null)
        {
            currentKart.transform.Rotate(Vector3.down * 30 * Time.deltaTime);
        }

        if (_isPlayerOne)
        {
            customColor = new Color(0.8679245f, 0.2990305f, 0.2865787f);
        }
        else if (!_isPlayerOne)
        {
            customColor = new Color(0.2862746f, 0.5077499f, 0.8666667f);
        }
    }

    public void HoverKart(int index)
    {
        if (index != currentIndex)
        {
            SpawnKart(index);
        }
    }

    private void SpawnKart(int index)
    {
        if (currentKart != null)
        {
            Destroy(currentKart);
        }

        currentKart = Instantiate(_karts[index], _kartTransform.position, _kartTransform.rotation);
        currentKart.transform.SetParent(_kartTransform);
        currentKart.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        currentIndex = index;
    }

    public void ConfirmSelection(Button KartButton)
    {
        indexPlayers += 1;

        if (indexPlayers == 1)
        {
            _buttons[currentIndex].interactable = false;
            _buttonsOutliners[currentIndex].color = customColor;

            PlayerPrefs.SetInt("Kart_Player_001", currentIndex);
            PlayerPrefs.Save();

            _isPlayerOne = false;

            _playerChoosingText001.SetActive(false);
            _playerChoosingText002.SetActive(true);
        }
        if (indexPlayers == 2)
        {
            _buttons[currentIndex].interactable = false;
            _buttonsOutliners[currentIndex].color = customColor;

            PlayerPrefs.SetInt("Kart_Player_002", currentIndex);
            PlayerPrefs.Save();

            DisplayStartGameMenu();
        }
    }

    public void DisplayStartGameMenu()
    {
        _startMenu.SetActive(true);
    }

    public void ReturnSelectionMenu()
    {
        currentIndex = PlayerPrefs.GetInt("Kart_Player_001", 0);
        currentIndex = PlayerPrefs.GetInt("Kart_Player_002", 1);

        _playerChoosingText001.SetActive(true);
        _playerChoosingText002.SetActive(false);

        SpawnKart(currentIndex);
        _isPlayerOne = true;
        indexPlayers = 0;

        int indexA = 0;
        while(indexA < _buttons.Length)
        {
            _buttons[indexA].interactable = true;
            indexA++;
        }

        int indexB = 0; ;
        while (indexB < _buttons.Length)
        {
            _buttonsOutliners[indexB].color = Color.white;
            indexB++;
        }

        _startMenu.SetActive(false);
    }
}