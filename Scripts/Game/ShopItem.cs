using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public void MouseDown(int id)
    {
        if (Inventory.Instance.Items[id] > 0)
        {
            Inventory.Instance.Items[id]--;
            PlayerBalance.Instance.ChangeCoins(Shop.Instance.Prices[id]);
            ShopUI.Instance.UpdateInventory();
            HeaderUI.Instance.UpdateCoins();
            ShopUI.Instance.AnimItem(id);
            Audio.Instance.PlaySFX(Audio.Instance.ItemBought, 0.6f);
        }
    }
}
