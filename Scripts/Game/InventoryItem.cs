using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private int _id;
    public void MouseDown(int id)
    {
        _id = id;
        if (Inventory.Instance.Items[id] > 0)
        {
            Audio.Instance.PlaySFX(Audio.Instance.ItemSelected, 0.6f);
            WorkbenchControll.Instance.SetDragObject(id);
        }
    }
    public void MouseUp() 
    {
        if (Inventory.Instance.Items[_id] > 0)
        {
            Audio.Instance.PlaySFX(Audio.Instance.ItemSetted, 0.6f);
            WorkbenchControll.Instance.ResetDragObject();
        }
    }
}
