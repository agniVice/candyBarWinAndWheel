using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public static Workbench Instance { get; private set; }

    public List<int> Components;
    public List<GameObject> Items;

    public int[] Energy;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else Instance = this;
    }
    public bool CanAddComponent(int id)
    {
        if (Components.Count < 4)
        {
            if (Components.Contains(3))
            {
                if (id != 3)
                    return true;
                else
                    return false;
            }
            else
            {
                if (Components.Count != 3)
                    return true;
                else
                {
                    if (id == 3)
                        return true;
                    else
                        return false;
                }
            }
        }
        else
            return false;
    }
    public void Add(int id, GameObject go)
    {
        Components.Add(id);
        Items.Add(go);
        WorkbenchUI.Instance.UpdateBench();
    }
    public void Remove(int id, GameObject gameObject)
    {
        Components.Remove(id);
        Items.Remove(gameObject);
        WorkbenchUI.Instance.UpdateBench();
    }
    public bool GetCompleted()
    {
        if (Components.Count < 2)
            return false;
        if (Components.Contains(3))
            return true;
        else
            return false;
    }
    public void OnCandyComplete()
    {
        Audio.Instance.PlaySFX(Audio.Instance.Correct, 0.6f);
        PlayerBalance.Instance.ChangeEnergy(GetEnergyFromCandy());
        HeaderUI.Instance.UpdateEnergy();
        Components.Clear();
        Items.Clear();
    }
    public int GetEnergyFromCandy()
    {
        int energy = 0;
        for (int i = 0; i < Components.Count; i++) 
        {
            if (Components[i] == 0)
                energy += Energy[0];
            if (Components[i] == 1)
                energy += Energy[1];
            if (Components[i] == 2)
                energy += Energy[2];
            if (Components[i] == 3)
                energy += Energy[3];
        }
        return energy;
    }
}
