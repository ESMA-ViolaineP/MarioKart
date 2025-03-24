using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
   public void LoadCircuit(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
