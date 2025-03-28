using UnityEngine;

[CreateAssetMenu(fileName = "ItemLightning", menuName = "Scriptable Objects/ItemLightning")]
public class ItemLightning : Item
{
    public override void Activation(PlayerItemManager player)
    {
        PlayerDisplay myPlayer = player.GetComponent<PlayerDisplay>();

        PlayerDisplay[] allPlayers = FindObjectsByType<PlayerDisplay>(FindObjectsSortMode.None);

        foreach (PlayerDisplay thisPlayer in allPlayers)
        {
            if (thisPlayer != myPlayer)
            {
                var playerKart = thisPlayer.GetComponent<KartController>();
                playerKart.LightningEffect(2, 3);
            }
        }
    }
}
