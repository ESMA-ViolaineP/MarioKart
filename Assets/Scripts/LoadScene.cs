using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadImage;

   public void LoadCircuit(string sceneName)
   {
        StartCoroutine (LoadingScreen(sceneName));
   }

    private IEnumerator LoadingScreen(string sceneName)
    {
        _loadImage.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
