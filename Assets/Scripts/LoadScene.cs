using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadImage;

   public void LoadCircuit(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        _loadImage.SetActive(true);
    }
}
