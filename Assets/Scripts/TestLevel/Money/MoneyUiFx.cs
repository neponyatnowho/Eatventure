using DG.Tweening;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class MoneyUiFx : MoneyFx
{
    [SerializeField] private TMP_Text _text;
    private Sequence _aminationOut;
    private Vector3 _startScale;
    private void Awake()
    {
        _startScale = transform.localScale;
        transform.rotation = Camera.main.transform.rotation;
    }
    public void Show(double money, Vector3 pos)
    {
        ResetStats();
        transform.position = pos;
        _text.text = NumbersFormatter.Format(money);
        AnimateOut();
    }

    private void AnimateOut()
    {
        _aminationOut = DOTween.Sequence();
        _aminationOut.Append(transform.DOLocalMoveY(2f, 0.7f));
        _aminationOut.Append(transform.DOScale(0f, 0.3f));
        _aminationOut.AppendCallback(() => gameObject.SetActive(false));
        _aminationOut.Append(transform.DOScale(_startScale.y, 0f));
        _aminationOut.Play();
    }

    private void ResetStats()
    {
        if(_aminationOut != null)
        {
            _aminationOut.Kill();
            _aminationOut = null;
        }

        gameObject.SetActive(true);
        transform.localScale = _startScale;

    }
}
