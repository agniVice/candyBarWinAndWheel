using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaderUI : MonoBehaviour
{
    public static HeaderUI Instance { get; private set; }

    [SerializeField] private CanvasGroup _spinWin;
    [SerializeField] private TextMeshProUGUI _spinWinText;

    [SerializeField] private TextMeshProUGUI _coins;
    [SerializeField] private TextMeshProUGUI _energy;

    [SerializeField] private Image _energyBar;

    private float _lastCoins = 0;
    private float _currentCoins;

    private float _lastEnergy = 0;
    private float _currentEnergy;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        UpdateEnergy(0.5f, 2f);
        UpdateCoins(0.5f, 2f);
    }
    public void UpdateCoins(float delay = 0f, float time = 0.5f)
    {
        _currentCoins = _lastCoins;
        _lastCoins = PlayerBalance.Instance.Coins;
        DOTween.To(() => _currentCoins, x => _currentCoins = x, PlayerBalance.Instance.Coins, time).SetEase(Ease.OutCubic).OnUpdate(OnUpdateCoins).SetDelay(delay);
    }
    public void OnUpdateCoins()
    {
        _coins.text = Mathf.Round(_currentCoins).ToString();
    }
    public void OnEnergyFull()
    {
        _spinWin.DOFade(1, 0.3f).SetLink(_spinWin.gameObject);
        _spinWin.DOFade(0, 0.3f).SetLink(_spinWin.gameObject).SetDelay(0.5f);
        _spinWinText.text = "+1Spin";

        _currentEnergy = _lastEnergy;
        _lastEnergy = PlayerBalance.Instance.Energy;
        DOTween.To(() => _currentEnergy, x => _currentEnergy = x, PlayerBalance.Instance.GetEnergyToSpin(), 0.5f).SetEase(Ease.OutCubic).OnUpdate(OnUpdateEnergy);
    }
    public void UpdateEnergy(float delay = 0f, float time = 0.5f)
    {
        _currentEnergy = _lastEnergy;
        _lastEnergy = PlayerBalance.Instance.Energy;
        DOTween.To(() => _currentEnergy, x => _currentEnergy = x, PlayerBalance.Instance.Energy, time).SetEase(Ease.OutCubic).OnUpdate(OnUpdateEnergy).SetDelay(delay);
    }
    private void OnUpdateEnergy()
    {
        _energyBar.fillAmount = _currentEnergy / (float)PlayerBalance.Instance.GetEnergyToSpin();
        _energy.text = Mathf.Round(_currentEnergy) + "/" + PlayerBalance.Instance.GetEnergyToSpin();
    }
}
