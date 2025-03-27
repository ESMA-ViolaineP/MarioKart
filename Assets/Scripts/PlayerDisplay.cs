using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject inkSplash;


    private Vector3 originalScale;
    private float originalYPosition;
    private bool isShrunken = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void InkSplash()
    {
       StartCoroutine (InkSplashRoutine()); 
    }

    private IEnumerator InkSplashRoutine()
    {
        inkSplash.SetActive(true);
        yield return new WaitForSeconds(3);
        inkSplash.SetActive(false);
    }

    public void LightningEffect(int ShrinkFactor, int ItemDelay)
    {
        if (!isShrunken)
        {
            StartCoroutine(LightningRoutine(ShrinkFactor, ItemDelay));
        }
    }

    private IEnumerator LightningRoutine(int ShrinkFactor, int ItemDelay)
    {
        isShrunken = true;

        originalYPosition = transform.position.y;

        transform.localScale = originalScale / ShrinkFactor;

        float newScale = (originalScale.y - transform.localScale.y) / 2;
        transform.position = new Vector3(transform.position.x, originalYPosition - newScale, transform.position.z);

        yield return new WaitForSeconds(ItemDelay);

        transform.localScale = originalScale;
        transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z);

        isShrunken = false;
    }
}
