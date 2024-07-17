using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop Instance {  get; private set; }

    public int[] Prices;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void OnBuyItem(int id)
    {
        PlayerBalance.Instance.ChangeCoins(-Prices[id]);
        Inventory.Instance.ChangeItem(id, 1);
        ShopUI.Instance.UpdateInventory();
        HeaderUI.Instance.UpdateCoins();
        ShopUI.Instance.AnimItem(id);
        Audio.Instance.PlaySFX(Audio.Instance.ItemBought, 0.6f);
    }
}
