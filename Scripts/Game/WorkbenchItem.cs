using DG.Tweening;
using UnityEngine;

public class WorkbenchItem : MonoBehaviour
{
    public int Id;
    private bool _isRemoved;
    public void MouseDown()
    {

        if (_isRemoved)
            return;
        Audio.Instance.PlaySFX(Audio.Instance.ItemSelected, 0.6f);
        _isRemoved = true;
        Inventory.Instance.ChangeItem(Id, 1);
        Workbench.Instance.Remove(Id, gameObject);
        transform.SetParent(WorkbenchControll.Instance.StartPositions[Id].parent);
        transform.SetSiblingIndex(0);
        transform.DOMove(WorkbenchControll.Instance.StartPositions[Id].position, 0.5f).SetLink(gameObject).SetEase(Ease.InBack).OnKill(() => { WorkbenchUI.Instance.UpdateInventory(); });
        Destroy(gameObject, 0.6f);
    }
}