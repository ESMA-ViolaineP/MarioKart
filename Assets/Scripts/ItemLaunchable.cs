using UnityEngine;

[CreateAssetMenu(fileName = "ItemLaunchable", menuName = "Scriptable Objects/ItemLaunchable")]
public class ItemLaunchable : Item
{
    public GameObject ObjectToLaunch;

    public override void Activation(PlayerItemManager player)
    {
        Instantiate(ObjectToLaunch, player.transform.position, player.transform.rotation);
    }

}
