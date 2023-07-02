using DG.Tweening;
using System;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private OrderCanvasActor _orderCanvas;
    private Vector3 _endPoint;
    private IOrder _order;
    private float _movementSpeed = 2f;

    public void Init(Vector3 endPoint)
    {
        _endPoint = endPoint;
    }

    public void SetOrder(IOrder order)
    {
        _order = order;
    }
    public Tween GoToClientPoint(UnitPoint<Client> point)
    {
        return Move(point.transform.position, point.LookAtPoint);
    }

    public IOrder ShowOrder()
    {
        _orderCanvas.ShowOrder(_order.OrderType);
        return _order;
    }

    public void GetOrder()
    {
        _orderCanvas.HideOrder();
        GoToEnd();
    }

    public void GoToEnd()
    {
        Move(_endPoint, _endPoint)
            .OnComplete(() => gameObject.SetActive(false));
    }

    private Tween Move(Vector3 movePoint, Vector3 lookPoint)
    {
        float distance = Vector3.Distance(transform.position, movePoint);
        float duration = distance / _movementSpeed;

        var sequence = DOTween.Sequence();

        sequence
            .Append(transform.DOLookAt(lookPoint, 0.5f))
            .Join(transform.DOMove(movePoint, duration))
            .AppendInterval(0.1f);

        if (movePoint != lookPoint)
        {
            sequence.Append(transform.DOLookAt(lookPoint, 0.1f));
        }

        sequence.SetEase(Ease.InOutQuad);
        sequence.Play();

        return sequence;
    }

}
