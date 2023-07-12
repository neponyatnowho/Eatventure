using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CheckoutTable : MonoBehaviour
{

    public Action<CheckoutTable> OnReadyToOrder;
    public Action<Order> OnOrderShowed;
    public Action<Order> OnOrderClosed;

    [SerializeField] private WorkPoint _workerPoint;
    [SerializeField] private QueuePoint _queuePoint;
    [SerializeField] private ClientCheckoutPoint _clientPoint;
    [SerializeField] public int tablenumber;
    private Client _currentClient;
    private Order _currentOrder;
    public ClientCheckoutPoint ClientPoint => _clientPoint;
    public Vector3 WorkerPointPosition => _workerPoint.transform.position;

    public bool IsClientCheckoutPointExist()
    {
        return _clientPoint.IsFree; 
    }

    public bool IsQueuePointExist()
    {
        return _queuePoint.IsFree;
    }

    public UnitPoint<Client> GetAndReservFreePoint(Client client)
    {
        if(_clientPoint.IsFree)
        {
            ReserveClientPoint(client);
            return _clientPoint;
        }
        ReserveQueuePoint(client);
        return _queuePoint;
    }

    public void CloseClient()
    {
        OnOrderClosed?.Invoke(_currentOrder);
        ResetQueue();
        ShowMoneyFx();
    }

    public void ReserveClientPoint(Client client)
    {
        _currentClient = client;
        _clientPoint.Reserv(client);
        _currentClient.GoToClientPoint(_clientPoint)
            .OnComplete(() =>
            {
                OnReadyToOrder?.Invoke(this);
            }
            );
    }

    public void WriteOrder()
    {
        _currentOrder = _currentClient.ShowOrder();
        _currentOrder.OrderTable = this;
        OnOrderShowed?.Invoke(_currentOrder);
    }

    public void ReserveQueuePoint(Client client)
    {
        _queuePoint.Reserv(client);
        client.GoToClientPoint(_queuePoint);
    }
    private void ResetQueue()
    {
        _clientPoint.SetFree();

        if (!_queuePoint.IsFree)
        {
            ReserveClientPoint(_queuePoint.CurrentUnit);
            _queuePoint.SetFree();
        }
    }
    private void ShowMoneyFx()
    {
        //_moneyFx.gameObject.SetActive(true);
    }
}
