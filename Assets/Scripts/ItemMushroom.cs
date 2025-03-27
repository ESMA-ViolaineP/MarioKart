using UnityEngine;

[CreateAssetMenu(fileName = "ItemMushroom", menuName = "Scriptable Objects/ItemMushroom")]
public class ItemMushroom : Item
{
    [SerializeField]
    private Sprite twoMushrooms;
    [SerializeField]
    private Sprite oneMushroom;
    public override void Activation(PlayerItemManager player)
    {
        if (player.NumberOfItemUse == 3)
        {
            player.ItemImage.sprite = twoMushrooms;
        }
        else if (player.NumberOfItemUse == 2)
        {
            player.ItemImage.sprite = oneMushroom;
        }
        player.Kart.Boost(3);
    }
}
