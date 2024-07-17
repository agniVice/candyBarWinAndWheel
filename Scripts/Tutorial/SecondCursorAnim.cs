using DG.Tweening;
using UnityEngine;

public class SecondCursorAnim : MonoBehaviour
{
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _endPos;
    private void Start()
    {
        _startPos = transform.position;
        Anim();
    }
    private void Anim()
    {
        transform.DOMove(_endPos, 0.6f).SetEase(Ease.OutCubic).SetLink(gameObject).SetDelay(1f).OnKill(() => {
            transform.DOMove(_startPos, 0.4f).OnKill(() => { Anim();
        });
        transform.DOScale(0.8f, 0.4f).SetEase(Ease.OutBack).SetLink(gameObject).SetDelay(1f).OnKill(() => {
            transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).SetLink(gameObject).SetDelay(0.4f); });
        });
    }
}
