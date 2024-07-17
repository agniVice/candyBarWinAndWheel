using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }

    [Header("Shop")]
    [SerializeField] private CanvasGroup _errorPanel;
    [SerializeField] private TextMeshProUGUI _errorCoinsText;
    [SerializeField] private List<TextMeshProUGUI> _priceTexts;

    [Header("Inventory")]
    [SerializeField] private List<TextMeshProUGUI> _itemsCount;
    [SerializeField] private List<Transform> _items;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void OnBuyButtonClicked(int id)
    {
        if (PlayerPrefs.GetInt("3Tutorial", 1) == 0 && PlayerPrefs.GetInt("4Tutorial", 1) == 1)
        {
            PlayerPrefs.SetInt("4Tutorial", 0);
            Tutorial.Instance.Cursors[3].SetActive(false);
            Tutorial.Instance.Cursors[4].SetActive(true);
        }
        if (PlayerPrefs.GetInt("3Tutorial", 1) == 1)
        {
            PlayerPrefs.SetInt("3Tutorial", 0);
            Tutorial.Instance.Cursors[2].SetActive(false);
            Tutorial.Instance.Cursors[3].SetActive(true);
        }
        if (Shop.Instance.Prices[id] > PlayerBalance.Instance.Coins)
        {
            OpenError(Shop.Instance.Prices[id] - PlayerBalance.Instance.Coins);
            return;
        }
        AnimItem(id);
        Shop.Instance.OnBuyItem(id);
    }
    public void AnimItem(int id)
    {
        _items[id].transform.DOScale(1.2f, 0.2f).SetLink(_items[id].gameObject).SetEase(Ease.OutBack);
        _items[id].transform.DOScale(1f, 0.2f).SetLink(_items[id].gameObject).SetEase(Ease.OutBack).SetDelay(0.3f);
    }
    private void OpenError(int coins)
    {
        _errorCoinsText.text = coins.ToString();
        _errorPanel.alpha = 0f;
        _errorPanel.blocksRaycasts = true;
        _errorPanel.DOFade(1, 0.3f).SetLink(_errorPanel.gameObject);
    }
    public void CloseError()
    {
        _errorPanel.blocksRaycasts = false;
        _errorPanel.DOFade(0, 0.3f).SetLink(_errorPanel.gameObject);
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < _itemsCount.Count; i++)
        {
            _priceTexts[i].text = Shop.Instance.Prices[i].ToString();
            _itemsCount[i].text = Inventory.Instance.Items[i].ToString();
        }
    }
}
