using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuResults : MonoBehaviour
{
    [SerializeField]
    private Image _playerImage01, _playerImage02;
    [SerializeField]
    private TextMeshProUGUI _playerName01, _playerName02;
    [SerializeField]
    private TextMeshProUGUI _playerPoints01, _playerPoints02;
    [SerializeField]
    private TextMeshProUGUI _gainPoints01, _gainPoints02;
    [SerializeField]
    private GameObject _continueMenu;

    private Sprite imagePlayer01, imagePlayer02;
    private string namePlayer01, namePlayer02;

    void Start()
    {
        imagePlayer01 = GameManager.Instance.ImageKart[GameManager.Instance.SelectionPlayer01];
        imagePlayer02 = GameManager.Instance.ImageKart[GameManager.Instance.SelectionPlayer02];

        namePlayer01 = GameManager.Instance.NamePlayer01;
        namePlayer02 = GameManager.Instance.NamePlayer02;

        StartCoroutine(Results());
    }

    private IEnumerator Results()
    {
        if (GameManager.Instance.CircuitManager.WinPlayer001)
        {
            _playerImage01.sprite = imagePlayer01;
            _playerImage02.sprite = imagePlayer02;

            _playerName01.text = "<color=#DD4C49>" + namePlayer01 + "</color>";
            _playerName02.text = "<color=#4981DD>" + namePlayer02 + "</color>";

            _playerPoints01.text = "" + GameManager.Instance.PointsPlayer02;
            _playerPoints02.text = "" + GameManager.Instance.PointsPlayer01;

            GameManager.Instance.PointsPlayer01 += 15;
            GameManager.Instance.PointsPlayer02 += 12;
        }
        else
        {
            _playerImage01.sprite = imagePlayer02;
            _playerImage02.sprite = imagePlayer01;

            _playerName01.text = "<color=#4981DD>" + namePlayer02 + "</color>";
            _playerName02.text = "<color=#DD4C49>" + namePlayer01 + "</color>";

            _playerPoints01.text = "" + GameManager.Instance.PointsPlayer02;
            _playerPoints02.text = "" + GameManager.Instance.PointsPlayer01;

            GameManager.Instance.PointsPlayer02 += 15;
            GameManager.Instance.PointsPlayer01 += 12;
        }

        yield return new WaitForSeconds(3);

        _gainPoints01.text = "|";
        _gainPoints02.text = "|";

        if (GameManager.Instance.PointsPlayer01 > GameManager.Instance.PointsPlayer02)
        {
            _playerImage01.sprite = imagePlayer01;
            _playerImage02.sprite = imagePlayer02;

            _playerName01.text = "<color=#DD4C49>" + namePlayer01 + "</color>";
            _playerName02.text = "<color=#4981DD>" + namePlayer02 + "</color>";

            _playerPoints01.text = "" + GameManager.Instance.PointsPlayer01;
            _playerPoints02.text = "" + GameManager.Instance.PointsPlayer02;
        }
        else
        {
            _playerImage01.sprite = imagePlayer02;
            _playerImage02.sprite = imagePlayer01;

            _playerName01.text = "<color=#4981DD>" + namePlayer02 + "</color>";
            _playerName02.text = "<color=#DD4C49>" + namePlayer01 + "</color>";

            _playerPoints01.text = "" + GameManager.Instance.PointsPlayer02;
            _playerPoints02.text = "" + GameManager.Instance.PointsPlayer01;
        }

        yield return new WaitForSeconds(4);

        _continueMenu.SetActive(true);
    }
}
