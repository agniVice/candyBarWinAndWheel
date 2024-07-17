using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteUI : MonoBehaviour
{
    public static RouletteUI Instance { get; private set; }

    [SerializeField] private Image _roulette;
    [SerializeField] private Vector3[] _rotations;
    [SerializeField] private TextMeshProUGUI _spinsText;

    [SerializeField] private Sprite _spinActiveSprite;
    [SerializeField] private Sprite _spinInactiveSprite;

    [SerializeField] private Button _menuButton;
    [SerializeField] private Image _spinButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void Start()
    {
        UpdateSpins();
        if (PlayerPrefs.GetInt("1Tutorial", 1) == 1)
            Tutorial.Instance.Cursors[0].SetActive(true);
    }
    public void UpdateSpins()
    {
        _spinsText.transform.DOScale(1.2f, 0.2f).SetLink(_spinsText.gameObject).SetEase(Ease.OutBack);
        _spinsText.transform.DOScale(1f, 0.2f).SetLink(_spinsText.gameObject).SetEase(Ease.OutBack).SetDelay(0.3f);
        _spinsText.text = PlayerBalance.Instance.Spins + " SPINS";
    }
    public void OnRouletteSpin()
    {
        Audio.Instance.PlaySFX(Audio.Instance.Spin, 0.3f);
        Audio.Instance.PlaySFX(Audio.Instance.StartSpin, 0.6f);
        _menuButton.interactable = false;
        _roulette.transform.DORotate(new Vector3(0, 0, 3600) + _rotations[Roulette.Instance.WinReward], Roulette.Instance.SpinTime, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
        _spinButton.sprite = _spinInactiveSprite;
        UpdateSpins();
    }
    public void OnRouletteEndSpin()
    {
        if (PlayerPrefs.GetInt("2Tutorial", 1) == 1)
            Tutorial.Instance.Cursors[1].SetActive(true);
        Audio.Instance.PlaySFX(Audio.Instance.StartSpin, 0.6f);
        _menuButton.interactable = true;
        _spinButton.sprite = _spinActiveSprite;
        UpdateSpins();
    }
    public void OnSpinButtonClicked()
    {
        if (PlayerPrefs.GetInt("1Tutorial", 1) == 1)
        {
            PlayerPrefs.SetInt("1Tutorial", 0);
            Tutorial.Instance.Cursors[0].SetActive(false);
        }
        Roulette.Instance.Spin();
    }
}
