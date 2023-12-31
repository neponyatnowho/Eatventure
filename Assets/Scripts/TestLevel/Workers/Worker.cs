﻿using DG.Tweening;
using System;
using UnityEngine;

public class Worker : MonoBehaviour
{ 
    [SerializeField] private float _movementSpeed;
    [SerializeField] private TimerCanvas _timerCanvas;

    public event Action OnCheckoutComplete;
    public event Action OnCookingComplete;
    public event Action OnOrderComplete;

    private bool _isFree = true;
    public bool IsFree => _isFree;

    private Order _currentOrder;
    private UnitPoint<Worker> _currentWorkPoint;
    public void TakeOrder(CheckoutTable table)
    {
        _isFree = false;

        if (transform.position != table.WorkerPointPosition)
        {
            Move(table.WorkerPointPosition, table.transform.position)
                .OnComplete(() => ProcesedOrder(table));
        }
        else
        {
            ProcesedOrder(table);
        }
    }
    private void ProcesedOrder(CheckoutTable table)
    {
        var sequence = DOTween.Sequence();
        _timerCanvas.ShowTimer(2f);
        sequence.Append(transform.DOScale(1.1f, 1f));
        sequence.Append(transform.DOScale(1f, 1f));
        sequence.OnComplete(() => 
        { 
            _isFree = true;
            table.WriteOrder();
            OnCheckoutComplete?.Invoke();
        });
        sequence.Play();
    }
    public void MakeOrder(UnitPoint<Worker> point, Order order, float time)
    {
        _isFree = false;
        _currentOrder = order;
        _currentWorkPoint = point;
        Move(_currentWorkPoint.transform.position, _currentWorkPoint.LookAtPoint)
        .OnComplete((() => Cooking(time)));
    }

    private Tween Cooking(float time)
    {
        _timerCanvas.ShowTimer(time);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.1f, time/2f));
        sequence.Append(transform.DOScale(1f, time / 2f));

        sequence.Play();

        sequence.OnComplete(() =>
        {
            _currentWorkPoint.SetFree();
            OnCookingComplete?.Invoke();
            GiveOrder();
        });
        return sequence;
    }

    public Tween GiveOrder()
    {
        var sequence = DOTween.Sequence();
        Move(_currentOrder.OrderTable.WorkerPointPosition, _currentOrder.OrderTable.transform.position)
        .OnComplete(() => 
        { 
            _isFree = true;
            _currentOrder.OrderTable.ClientPoint.CurrentUnit.GetOrder();
            _currentOrder.OrderTable.CloseClient();
            OnOrderComplete?.Invoke(); 
        }
        );
        return sequence;
    }

    private Tween Move(Vector3 movePoint, Vector3 lookPoint)
    {
        float distance = Vector3.Distance(transform.position, movePoint);
        float duration = distance / _movementSpeed;

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLookAt(lookPoint, duration / 2f))
            .Join(transform.DOMove(movePoint, duration))
            .Append(transform.DOLookAt(lookPoint, 0.2f));

        sequence.SetEase(Ease.InOutQuad);
        sequence.Play();

        return sequence;
    }
}
