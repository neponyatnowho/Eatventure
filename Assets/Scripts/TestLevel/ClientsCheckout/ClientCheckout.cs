using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientCheckout : MonoBehaviour
{
    public Action OnReadyTableAdded;

    [SerializeField] private ClientObserver _clientObserver;
    [SerializeField] private MoneyController _moneyController;
    [SerializeField] private CheckoutTable[] _tables;
    [SerializeField] private MoneyFxController _moneyFx;

    private Queue<CheckoutTable> _readyToOrderTables = new Queue<CheckoutTable>();
    private Queue<IOrder> _readyToMakeOrders = new Queue<IOrder>();

    public Queue<CheckoutTable> ReadyToOrderTables => _readyToOrderTables;
    public Queue<IOrder> ReadyToMakeOrders => _readyToMakeOrders;


    private void Awake()
    {
        _clientObserver.WhenTrigerEnter += NewClientEnter;
        foreach (var table in _tables)
        {
            table.OnReadyToOrder += AddTableToQueue;
            table.OnOrderShowed += AddOrderToQueue;
            table.OnOrderClosed += Cashing;
            table.OnOrderClosed += MoneyFx;
        }
    }

    private void NewClientEnter(Client client)
    {
        foreach (var table in _tables)
        {
            if(table.IsClientCheckoutPointExist())
            {
                table.GetAndReservFreePoint(client);
                return;
            }
        }
        foreach (var table in _tables)
        {
            if (table.IsQueuePointExist())
            {
                table.GetAndReservFreePoint(client);
                return;
            }
        }
        client.GoToEnd();
    }

    public void Cashing(IOrder order)
    {
        _moneyController.CashingTheOrder(order);
    }

    public void MoneyFx(IOrder order)
    {
        _moneyFx.ShowFx(order);
    }

    private void AddTableToQueue(CheckoutTable table)
    {
        _readyToOrderTables.Enqueue(table);
        OnReadyTableAdded?.Invoke();
    }

    private void AddOrderToQueue(IOrder order)
    {
        _readyToMakeOrders.Enqueue(order);
        
    }

    public IOrder GetReadyToMakeOrder()
    {
       return _readyToMakeOrders.Dequeue();
    }

    public CheckoutTable GetReadyToOrderTable()
    {
        return _readyToOrderTables.Dequeue();
    }

}
