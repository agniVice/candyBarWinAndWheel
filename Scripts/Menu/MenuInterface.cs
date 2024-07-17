using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInterface : MonoBehaviour
{
    public static MenuInterface Instance { get; private set; }

    [SerializeField] private CanvasGroup _menuPanel;
    [SerializeField] private CanvasGroup _settingsPanel;

    [Space]
    [SerializeField] private List<Transform> _menuTransforms = new List<Transform>();
    [SerializeField] private List<Transform> _settingsTransforms = new List<Transform>();

    [Space]
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;

    [SerializeField] private Image _soundButtonImage;
    [SerializeField] private Image _musicButtonImage;

    private void Awake()
    {
        if(Instance != this && Instance != null)
            Destroy(this);
        Instance = this;
    }
    private void Start() => OpenMenu(0.1f);
    private void OpenMenu(float openDelay = 0f)
    {
        _menuPanel.alpha = 0f;
        _menuPanel.blocksRaycasts = true;
        _menuPanel.DOFade(1, 0.3f).SetLink(_menuPanel.gameObject).SetDelay(openDelay);

        float delay = 0.2f;
        foreach (var item in _menuTransforms)
        {
            item.localScale = Vector2.zero;
            item.DOScale(Vector2.one, Random.Range(0.15f, 0.3f)).SetLink(item.gameObject).SetEase(Ease.OutBack).SetDelay(delay + openDelay);
            delay += 0.1f;
        }
    }
    private void CloseMenu()
    {
        _menuPanel.blocksRaycasts = false;
        _menuPanel.DOFade(0, 0.4f).SetLink(_menuPanel.gameObject);

        float delay = 0.1f;
        foreach (var item in _menuTransforms)
        {
            item.DOScale(Vector2.zero, Random.Range(0.2f, 0.3f)).SetLink(item.gameObject).SetEase(Ease.OutBack).SetDelay(delay);
            delay += 0.05f;
        }
    }
    private void OpenSettings()
    {
        CloseMenu();
        UpdateSettings();

        _settingsPanel.alpha = 0f;
        _settingsPanel.blocksRaycasts = true;
        _settingsPanel.DOFade(1, 0.3f).SetLink(_settingsPanel.gameObject);

        float delay = 0.1f;
        foreach (var item in _settingsTransforms)
        {
            item.localScale = Vector2.zero;
            item.DOScale(Vector2.one, Random.Range(0.2f, 0.3f)).SetLink(item.gameObject).SetEase(Ease.OutBack).SetDelay(delay);
            delay += 0.05f;
        }
    }
    private void CloseSettings()
    {
        _settingsPanel.blocksRaycasts = false;
        _settingsPanel.DOFade(0, 0.3f).SetLink(_settingsPanel.gameObject);

        float delay = 0.1f;
        foreach (var item in _settingsTransforms)
        {
            item.DOScale(Vector2.zero, Random.Range(0.2f, 0.3f)).SetLink(item.gameObject).SetEase(Ease.InBack).SetDelay(delay);
            delay += 0.05f;
        }
    }
    public void UpdateSettings()
    {
        if (Audio.Instance.Sound)
            _soundButtonImage.sprite = _enabledSprite;
        else
            _soundButtonImage.sprite = _disabledSprite;

        if (Audio.Instance.Music)
            _musicButtonImage.sprite = _enabledSprite;
        else
            _musicButtonImage.sprite = _disabledSprite;
    }
    public void OnPlayButtonClicked()
    {
        Audio.Instance.PlaySFX(Audio.Instance.LastSpin, 0.6f);
        CloseMenu();
        SceneLoader.Instance.ChangeScene(2, 0.1f + (0.05f * _menuTransforms.Count));
    }
    public void OnExitButtonClicked()
    {
        Audio.Instance.PlaySFX(Audio.Instance.LastSpin, 0.6f);
        CloseMenu();
        SceneLoader.Instance.CloseApp(0.1f + (0.05f * _menuTransforms.Count));
    }
    public void OnSettingsButtonClicked()
    {
        Audio.Instance.PlaySFX(Audio.Instance.LastSpin, 0.6f);
        OpenSettings();
    }
    public void OnMusicToggle() => Audio.Instance.MusicToggle();
    public void OnSoundToggle() => Audio.Instance.SoundToggle();
    public void OnReturnButtonClicked()
    {
        Audio.Instance.PlaySFX(Audio.Instance.LastSpin, 0.6f);
        CloseSettings();
        OpenMenu();
    }
}
