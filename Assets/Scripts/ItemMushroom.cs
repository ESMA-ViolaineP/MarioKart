using UnityEngine;

[CreateAssetMenu(fileName = "ItemMushroom", menuName = "Scriptable Objects/ItemMushroom")]
public class ItemMushroom : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.carController.Turbo(3);
    }
}
