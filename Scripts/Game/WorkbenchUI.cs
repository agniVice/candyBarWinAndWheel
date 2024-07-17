using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchUI : MonoBehaviour
{
    public static WorkbenchUI Instance { get; private set; }

    [Header("Workbench")]
    [SerializeField] private CanvasGroup _energyWin;
    [SerializeField] private TextMeshProUGUI _energyWinText;

    [SerializeField] private Button _collectButton;
    [SerializeField] private GameObject _candyPrefab;
    [SerializeField] private Transform _candyParent;

    [Header("Inventory")]
    [SerializeField] private List<TextMeshProUGUI> _itemsCount;

    private GameObject _candy;

    private void Awake()
    {
        if(Instance != this && Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    public void UpdateBench()
    {
        if (Workbench.Instance.GetCompleted())
            _collectButton.interactable = true;
        else
            _collectButton.interactable = false;
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < _itemsCount.Count; i++)
        {
            _itemsCount[i].text = Inventory.Instance.Items[i].ToString();
        }
    }
    public void OnCompleteButtonClicked()
    {
        if (Workbench.Instance.GetCompleted())
        {
            if (PlayerPrefs.GetInt("8Tutorial", 1) == 1)
                Tutorial.Instance.Cursors[7].SetActive(false);
            float delay = 0f;
            foreach (var item in Workbench.Instance.Items)
            {
                item.transform.DOScale(0, Random.Range(0.1f, 0.2f)).SetLink(item).SetEase(Ease.InBack).SetDelay(delay);
                delay += 0.05f;
            }
            _candy = Instantiate(_candyPrefab, _candyParent);
            _candy.GetComponent<Candy>().Initialize(Workbench.Instance.Items.Count, Workbench.Instance.Components);
            _candy.transform.localScale = Vector3.zero;
            _candy.transform.DOScale(1, 0.4f).SetLink(_candy).SetEase(Ease.OutBack).SetDelay(delay);
            _energyWin.DOFade(1, 0.3f).SetLink(_energyWin.gameObject);
            _energyWin.DOFade(0, 0.3f).SetLink(_energyWin.gameObject).SetDelay(0.5f);
            _energyWinText.text = "+" + Workbench.Instance.GetEnergyFromCandy();
            Workbench.Instance.OnCandyComplete();
            UpdateBench();

            _candy.transform.DOScale(0, 0.4f).SetLink(_candy).SetEase(Ease.InBack).SetDelay(delay + 1.5f);
            Destroy(_candy, delay + 2f);
        }
    }
}
