using UnityEngine;

[CreateAssetMenu(fileName = "ItemLaunchable", menuName = "Scriptable Objects/ItemLaunchable")]
public class ItemLaunchable : Item
{
    public GameObject objectToLaunch;

    public override void Activation(PlayerItemManager player)
    {
        Instantiate(objectToLaunch, player.transform.position, player.transform.rotation);
    }

}
