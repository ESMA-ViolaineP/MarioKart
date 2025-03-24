using UnityEngine;

public class KartColor : MonoBehaviour
{
    [Header("Mesh Reference")]
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [Header("Materials")]
    private int materialIndex = 3;

    [Header("Player")]
    [SerializeField]
    private bool _isPlayerOne;
    private int playerSelection;

    private void Start()
    {
        if (_isPlayerOne)
        {
            playerSelection = GameManager.Instance.SelectionPlayer01;
        }
        else
        {
            playerSelection = GameManager.Instance.SelectionPlayer02;
        }

        ChangeColorKart(playerSelection);
    }

    private void ChangeColorKart(int index)
    {
        Material[] materials = _meshRenderer.materials;
        materials[materialIndex] = GameManager.Instance.ColorMaterialKart[index];
        _meshRenderer.materials = materials;
    }
}
