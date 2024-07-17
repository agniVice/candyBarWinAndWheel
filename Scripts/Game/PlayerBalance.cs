using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    public static PlayerBalance Instance { get; private set; }

    public int Coins { get; private set; }
    public int Energy { get; private set; }
    public int Spins { get; private set; }

    [SerializeField] private int _energyToSpin;

    private void Awake()
    {
        if (Instance != null && Instance != null)
            Destroy(this);
        Instance = this;

        Initialize();
    }
    private void Initialize()
    {
        Coins = PlayerPrefs.GetInt("Coins", 20);
        Energy = PlayerPrefs.GetInt("Energy", 0);
        Spins = PlayerPrefs.GetInt("Spins", 10);
    }
    private void Save()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("Energy", Energy);
        PlayerPrefs.SetInt("Spins", Spins);
    }
    public void ChangeCoins(int count)
    {
        Coins += count;
        Save();
    }
    public void ChangeEnergy(int count)
    { 
        Energy += count;
        if (Energy >= _energyToSpin)
        {
            HeaderUI.Instance.OnEnergyFull();
            Energy-=_energyToSpin;
            Audio.Instance.PlaySFX(Audio.Instance.SpinAdded, 0.6f);
            ChangeSpins(1);
        }
        Save();
    }
    public void ChangeSpins(int count)
    { 
        Spins += count;
        Save();
    }
    public int GetEnergyToSpin() => _energyToSpin;
}
