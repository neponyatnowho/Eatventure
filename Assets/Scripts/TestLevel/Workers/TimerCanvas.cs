using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimerCanvas : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RotateToCamera _rotator;

    public void ShowTimer(float time)
    {
        _rotator.RotateLikeCamera();
        _canvas.enabled = true;

        _fillImage
            .DOFillAmount(1f, time)
            .SetEase(Ease.Linear)
            .OnComplete(CloseTimer);
    }

    private void CloseTimer()
    {
        _canvas.enabled = false;
        _fillImage.fillAmount = 0f;
    }
}
