using DG.Tweening;
using UnityEngine;

public class M_Camera : Singleton<M_Camera>
{
    [SerializeField] float shakeDuration = .5f;
    [SerializeField] float shakeStrength = .5f;

    [ContextMenu("Shake")]
    public void Shake()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength).SetEase(Ease.OutQuad);
    }
}
