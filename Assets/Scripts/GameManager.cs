using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Items")]
    public ItemScriptable CurrentItem;
    public ItemScriptable StockItem;
    public Image ImageSlot001, ImageSlot002;
    public bool Slot001Full, Slot002Full;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Slot001Full == false && Slot002Full == true)
        {
            ImageSlot001.sprite = StockItem.ItemImage;
            ImageSlot002.sprite = null;
            CurrentItem = StockItem;
            Slot002Full = false;
        }
    }
}
