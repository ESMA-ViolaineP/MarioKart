using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public int itemUseCount = 1;

    public virtual void Activation(PlayerItemManager player)
    {

    }
}
