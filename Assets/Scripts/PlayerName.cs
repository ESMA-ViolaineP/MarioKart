using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    [Header("Input Text")]
    [SerializeField]
    private TMP_InputField _inputText;

    [Header("Condition Player")]
    [SerializeField]
    private bool _isPlayerOne;

    public void SetName()
    {
        if (_isPlayerOne)
        {
            GameManager.Instance.NamePlayer01 = _inputText.text;

        }
        else
        {
            GameManager.Instance.NamePlayer02 = _inputText.text;
        }
    }
}
