using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemManager : MonoBehaviour
{
    [Header("Scripts")]
    public KartController Kart;
    public PlayerDisplay PlayerDisplay;

    [Header("Item")]
    [SerializeField]
    private List<Item> _itemList;
    [SerializeField]
    private Item _currentItem;
    [SerializeField]
    private string _itemInput;
    public Image ItemImage;
    public int NumberOfItemUse;

    private void Update()
    {
        if(Input.GetButtonDown(_itemInput))
        {
            UseItem();
        }
    }
    public void GenerateItem()
    {
        if(_currentItem == null)
        {
            StartCoroutine(DisplayItems());
        }
    }

    private IEnumerator DisplayItems()
    {
        int index = 0;

        while (index < 9)
        {
            ItemImage.sprite = _itemList[index%_itemList.Count].ItemSprite;
            index++;
            yield return new WaitForSeconds(0.1f);
        }

        _currentItem = _itemList[Random.Range(0, _itemList.Count)];
        ItemImage.sprite = _currentItem.ItemSprite;
        NumberOfItemUse = _currentItem.ItemUseCount;
    }

    public void UseItem()
    {
        if (_currentItem != null)
        {
            _currentItem.Activation(this);
            NumberOfItemUse--;

            if (NumberOfItemUse <= 0 )
            {
                _currentItem = null;
                ItemImage.sprite=null;
            }
        }
    }
}
