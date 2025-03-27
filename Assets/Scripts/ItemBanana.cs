using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBanana", menuName = "Scriptable Objects/ItemBanana")]
public class ItemBanana : Item
{
    [SerializeField]
    private GameObject _bananaToLaunch;

    public override void Activation(PlayerItemManager player)
    {
        Quaternion rotationItem = Quaternion.Euler(-90f, 0f, 0f);
        Vector3 behindPosition = player.transform.position - (player.transform.rotation * Vector3.forward * 2);

        Instantiate(_bananaToLaunch, behindPosition, rotationItem);
    }
}
