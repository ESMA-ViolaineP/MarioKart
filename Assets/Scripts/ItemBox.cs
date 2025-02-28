using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public MeshRenderer boxItemRenderer;
    public ItemScriptable[] AllItems;
    private CarController car;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        car = other.GetComponent<CarController>();
        boxItemRenderer.enabled = false;

        ItemScriptable randomItem = AllItems[Random.Range(0, AllItems.Length)];

        if (GameManager.Instance.Slot001Full == false)
        {
            StartCoroutine(GetItem001(randomItem));
        }
        else if (GameManager.Instance.Slot002Full == false)
        {
            StartCoroutine(GetItem002(randomItem));
        }
    }

    private IEnumerator GetItem001(ItemScriptable trueItem)
    {
        float i = 0;
        while (i < 15)
        {
            GameManager.Instance.ImageSlot001.sprite = AllItems[Random.Range(0, AllItems.Length)].ItemImage;
            yield return new WaitForSeconds(0.1f);
            i += 1;
        }
        GameManager.Instance.ImageSlot001.sprite = trueItem.ItemImage;
        GameManager.Instance.Slot001Full = true;
        car.ReceiveItem(trueItem);

        GameObject.Destroy(gameObject);
    }

    private IEnumerator GetItem002(ItemScriptable trueItem)
    {
        float i = 0;
        while (i < 15)
        {
            GameManager.Instance.ImageSlot002.sprite = AllItems[Random.Range(0, AllItems.Length)].ItemImage;
            yield return new WaitForSeconds(0.1f);
            i += 1;
        }
        GameManager.Instance.ImageSlot002.sprite = trueItem.ItemImage;
        GameManager.Instance.StockItem = trueItem;
        GameManager.Instance.Slot002Full = true;

        GameObject.Destroy(gameObject);
    }
}
