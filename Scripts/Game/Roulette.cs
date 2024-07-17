using UnityEngine;

public class Roulette : MonoBehaviour
{
    public static Roulette Instance { get; private set; }

    public int[] Rewards;
    public int[] WinRewards;
    public int WinReward {get; private set;}
    public float SpinTime;

    private float _currentTime;

    private bool _isSpinning;

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void FixedUpdate()
    {
        if (_isSpinning)
        {
            if (_currentTime > 0)
                _currentTime -= Time.fixedDeltaTime;
            else
                OnEndSpin();
        }
    }
    public void Spin()
    {
        if (_isSpinning)
            return;
        if (PlayerBalance.Instance.Spins <= 0)
            return;

        _isSpinning = true;
        _currentTime = SpinTime;

        PlayerBalance.Instance.ChangeSpins(-1);

        if (PlayerBalance.Instance.Spins <= 3 && PlayerBalance.Instance.Coins <= 50)
            WinReward = GetRandomWinReward();
        else
            WinReward = GetRandomReward();

        if (PlayerPrefs.GetInt("1Tutorial", 1) == 1)
        { 
            WinReward = 6;
            PlayerPrefs.SetInt("1Tutorial", 0);
        }

        RouletteUI.Instance.OnRouletteSpin();

    }
    private void OnEndSpin()
    {
        PlayerBalance.Instance.ChangeCoins(Rewards[WinReward]);
        RouletteUI.Instance.OnRouletteEndSpin();
        HeaderUI.Instance.UpdateCoins();
        _isSpinning = false;
    }
    public int GetRandomWinReward() => WinRewards[Random.Range(0, WinRewards.Length)];
    public int GetRandomReward() => Random.Range(0, Rewards.Length);
    public bool IsSpinning() => _isSpinning;
}
