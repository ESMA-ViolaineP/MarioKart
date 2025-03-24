using UnityEngine;

[CreateAssetMenu(fileName = "ItemStar", menuName = "Scriptable Objects/ItemStar")]

public class ItemStar : Item
{
    public override void Activation(PlayerItemManager player)
    {
        player.Kart.Boost(3);
    }
}
