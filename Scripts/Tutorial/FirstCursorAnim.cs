using DG.Tweening;
using UnityEngine;

public class FirstCursorAnim : MonoBehaviour
{
    private void Start()
    {
        Anim();
    }
    private void Anim()
    {
        transform.DOScale(0.8f, 0.4f).SetEase(Ease.OutBack).SetLink(gameObject).SetDelay(1f).OnKill(() => {
            transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).SetLink(gameObject).SetDelay(0.4f).OnKill(() => { Anim(); });
        });
    }
}
