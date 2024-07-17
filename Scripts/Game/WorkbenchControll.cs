using System.Collections.Generic;
using UnityEngine;

public class WorkbenchControll : MonoBehaviour
{
    public static WorkbenchControll Instance {  get; private set; }

    public Transform[] StartPositions;

    [SerializeField] private Transform _objectParent;
    [SerializeField] private GameObject[] _objectPrefabs;
    [SerializeField] private Collider2D _dropZone;

    [SerializeField] private List<WorkbenchItem> _workbenchItems;

    private Transform _dragObject;

    private bool _isDragging;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    private void Update()
    {
        if (_isDragging && _dragObject != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0.6f, 0);
            _dragObject.position = mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            if (_dropZone.OverlapPoint(_dragObject.position))
            {
                int id = _dragObject.GetComponent<WorkbenchItem>().Id;
                if (Workbench.Instance.CanAddComponent(id))
                {
                    if (PlayerPrefs.GetInt("7Tutorial", 1) == 1 && PlayerPrefs.GetInt("6Tutorial", 1) == 0)
                    {
                        PlayerPrefs.SetInt("7Tutorial", 0);
                        Tutorial.Instance.Cursors[6].SetActive(false);
                        Tutorial.Instance.Cursors[7].SetActive(true);
                    }
                    if (PlayerPrefs.GetInt("6Tutorial", 1) == 1)
                    {
                        PlayerPrefs.SetInt("6Tutorial", 0);
                        Tutorial.Instance.Cursors[5].SetActive(false);
                        Tutorial.Instance.Cursors[6].SetActive(true);
                    }
                    Inventory.Instance.ChangeItem(id, -1);
                    Workbench.Instance.Add(id, _dragObject.gameObject);
                    ResetDragObject();
                }
                else
                {
                    Destroy(_dragObject.gameObject);
                    ResetDragObject();
                }
                WorkbenchUI.Instance.UpdateInventory();
            }
            else
            {
                Destroy(_dragObject.gameObject);
                ResetDragObject();
            }
        }
    }
    public void SetDragObject(int id)
    {
        _dragObject = Instantiate(_objectPrefabs[id], _objectParent).transform;
        _dragObject.GetComponent<WorkbenchItem>().Id = id;
        _isDragging = true;
    }
    public void ResetDragObject()
    {
        Audio.Instance.PlaySFX(Audio.Instance.ItemSetted, 0.6f);
        _isDragging = false;
        _dragObject = null;
    }
    public void RemoveAll()
    {
        foreach (var item in _workbenchItems)
            item.MouseDown();
    }
}
