using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KartSelection : MonoBehaviour
{
    [Header("Kart")]
    [SerializeField]
    private GameObject _kart;
    [SerializeField]
    private MeshRenderer _kartMeshRenderer;

    [Header("Materials")]
    private int currentColorIndex = 0;
    private int materialIndex = 3;

    [Header("Player")]
    [SerializeField]
    private bool _isPlayerOne;
    private int playerIndex;
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
        ApplyColor(0);
    }

    private void Update()
    {
        _kart.transform.Rotate(Vector3.down * 30 * Time.deltaTime);

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
        if (index != currentColorIndex)
        {
            ApplyColor(index);
        }
    }

    private void ApplyColor(int index)
    {
        Material[] materials = _kartMeshRenderer.materials;
        materials[materialIndex] = GameManager.Instance.ColorMaterialKart[index];
        _kartMeshRenderer.materials = materials;

        currentColorIndex = index;
    }

    public void ConfirmSelection(Button KartButton)
    {
        playerIndex += 1;

        if (playerIndex == 1)
        {
            _buttons[currentColorIndex].interactable = false;
            _buttonsOutliners[currentColorIndex].color = customColor;

            GameManager.Instance.SelectionPlayer01 = currentColorIndex;

            _isPlayerOne = false;
            _playerChoosingText001.SetActive(false);
            _playerChoosingText002.SetActive(true);
        }
        if (playerIndex == 2)
        {
            _buttons[currentColorIndex].interactable = false;
            _buttonsOutliners[currentColorIndex].color = customColor;

            GameManager.Instance.SelectionPlayer02 = currentColorIndex;

            ActivateStartMenu();
        }
    }

    public void ActivateStartMenu()
    {
        _startMenu.SetActive(true);
    }

    public void ReturnSelectionMenu()
    {
        _playerChoosingText001.SetActive(true);
        _playerChoosingText002.SetActive(false);

        ApplyColor(0);
        _isPlayerOne = true;
        playerIndex = 0;

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