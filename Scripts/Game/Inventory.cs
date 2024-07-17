using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public int[] Items { get; private set; } = new int[4];

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;

        for(int i = 0; i < Items.Length; i++)
            Items[i] = PlayerPrefs.GetInt(i+"Item", 0);
    }
    public void ChangeItem(int id, int count)
    {
        Items[id] += count;
        Save();
    }
    private void Save()
    {
        for (int i = 0; i < Items.Length; i++)
            PlayerPrefs.SetInt(i + "Item", Items[i]);
    }
}
