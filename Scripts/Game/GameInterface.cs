using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public static GameInterface Instance {get; private set;}

    private int _currentWindow = 0;

    [Header("Header")]
    [SerializeField] private CanvasGroup _header;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    [Space]
    [SerializeField] private CanvasGroup _bottom;
    [SerializeField] private List<CanvasGroup> _panels;

    // 0 - Roulette;
    // 1 - Shop;
    // 2 - Workbench;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        Instance = this;

        _currentWindow = PlayerPrefs.GetInt("CurrentWindow", 0);
    }
    private void Start()
    {
        UpdateWindow(0.2f);
        ShowHeader(0.2f);
    }
    private void UpdateWindow(float openDelay = 0f, float closeDelay = 0)
    {
        Audio.Instance.PlaySFX(Audio.Instance.LastSpin, 0.6f);
        for (int i = 0; i < _panels.Count; i++)
        {
            if (i != _currentWindow)
            {
                _panels[i].DOFade(0, 0.3f).SetLink(_panels[i].gameObject).SetDelay(closeDelay);
                _panels[i].blocksRaycasts = false;
            }
            else
            {
                _panels[i].DOFade(1, 0.3f).SetLink(_panels[i].gameObject).SetDelay(openDelay);
                _panels[i].blocksRaycasts = true;
            }
        }
        switch (_currentWindow)
        {
            case 0:
                RouletteUI.Instance.UpdateSpins();
                break;
            case 1:
                if (PlayerPrefs.GetInt("2Tutorial", 1) == 1)
                {
                    PlayerPrefs.SetInt("2Tutorial", 0);
                    Tutorial.Instance.Cursors[1].SetActive(false);
                }
                if (PlayerPrefs.GetInt("3Tutorial", 1) == 1)
                    Tutorial.Instance.Cursors[2].SetActive(true);
                ShopUI.Instance.UpdateInventory();
                break;
            case 2:
                if (PlayerPrefs.GetInt("5Tutorial", 1) == 1)
                {
                    PlayerPrefs.SetInt("5Tutorial", 0);
                    Tutorial.Instance.Cursors[4].SetActive(false);
                    Tutorial.Instance.Cursors[5].SetActive(true);
                }
                WorkbenchUI.Instance.UpdateBench();
                WorkbenchUI.Instance.UpdateInventory();
                break;
        }
    }
    public void ShowHeader(float delay = 0f)
    {
        _bottom.alpha = 0f;
        _header.alpha = 0f;
        _header.blocksRaycasts = true;
        _header.DOFade(1, 0.3f).SetLink(_header.gameObject).SetDelay(delay);
        _bottom.DOFade(1, 0.3f).SetLink(_bottom.gameObject).SetDelay(delay);
    }
    public void HideHeader(float delay = 0f)
    {
        _header.blocksRaycasts = false;
        _header.DOFade(0, 0.3f).SetLink(_header.gameObject).SetDelay(delay);
        _bottom.DOFade(0, 0.3f).SetLink(_bottom.gameObject).SetDelay(delay);
    }
    public void OnWindowOpenButtonClicked(int id)
    {
        _currentWindow = id;
        UpdateWindow();
        Save();
    }
    private void Save() => PlayerPrefs.SetInt("CurrentWindow", _currentWindow);
    public void OnMenuButtonClicked()
    {
        Audio.Instance.PlaySFX(Audio.Instance.LastSpin, 0.6f);
        WorkbenchControll.Instance.RemoveAll();
        Save();
        _currentWindow = -1;
        UpdateWindow();
        HideHeader();
        SceneLoader.Instance.ChangeScene(1, 0.5f);
    }
}
