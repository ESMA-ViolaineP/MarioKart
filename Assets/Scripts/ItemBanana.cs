using UnityEngine;

[CreateAssetMenu(fileName = "ItemBanana", menuName = "Scriptable Objects/ItemBanana")]
public class ItemBanana : Item
{
    [SerializeField]
    private GameObject _bananaToLaunch;

    public override void Activation(PlayerItemManager player)
    {
        Instantiate(_bananaToLaunch, player.transform.position, player.transform.rotation);
    }

}
