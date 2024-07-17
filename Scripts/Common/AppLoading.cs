using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppLoading : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _progressText;

    [SerializeField] private float _loadTime = 2;

    private float _currentLoad = 0;

    private void Start()
    {
        Load();
    }
    private void Load()
    {
        DOTween.To(() => _currentLoad, x => _currentLoad = x, 0.63f, _loadTime*0.63f).SetEase(Ease.OutCubic).OnUpdate(UpdateBar);
        DOTween.To(() => _currentLoad, x => _currentLoad = x, 1, _loadTime * 0.3f).SetEase(Ease.OutCubic).OnUpdate(UpdateBar).SetDelay(_loadTime*0.7f+0.5f);
        SceneLoader.Instance.ChangeScene(1, _loadTime + 0.5f);
    }
    private void UpdateBar()
    {
        _progressText.text = "LOADING " + Mathf.Round(_currentLoad * 100) + "%";
        _progressBar.fillAmount = _currentLoad;
    }
}