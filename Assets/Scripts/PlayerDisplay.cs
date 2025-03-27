using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject _inkSplash;
    public void InkSplash()
    {
       StartCoroutine (InkSplashRoutine()); 
    }

    private IEnumerator InkSplashRoutine()
    {
        _inkSplash.SetActive(true);
        yield return new WaitForSeconds(3);
        _inkSplash.SetActive(false);
    }
}
