using UnityEngine;

[CreateAssetMenu(fileName = "ItemBlooper", menuName = "Scriptable Objects/ItemBlooper")]

public class ItemBlooper : Item
{
    public override void Activation(PlayerItemManager player)
    {
        PlayerDisplay myPlayer = player.GetComponent<PlayerDisplay>();

        PlayerDisplay[] allPlayers = FindObjectsByType<PlayerDisplay>(FindObjectsSortMode.None);

        foreach (PlayerDisplay thisPlayer in allPlayers)
        {
            if (thisPlayer != myPlayer)
            {
                player.PlayerDisplay.InkSplash();
            }
        }
    }
}
