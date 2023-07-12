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
    private Queue<Order> _readyToMakeOrders = new Queue<Order>();

    public Queue<CheckoutTable> ReadyToOrderTables => _readyToOrderTables;
    public Queue<Order> ReadyToMakeOrders => _readyToMakeOrders;


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

    public void Cashing(Order order)
    {
        _moneyController.CashingTheOrder(order);
    }

    public void MoneyFx(Order order)
    {
        _moneyFx.ShowFx(order);
    }

    private void AddTableToQueue(CheckoutTable table)
    {
        _readyToOrderTables.Enqueue(table);
        OnReadyTableAdded?.Invoke();
    }

    private void AddOrderToQueue(Order order)
    {
        _readyToMakeOrders.Enqueue(order);
        
    }

    public Order GetReadyToMakeOrder()
    {
       return _readyToMakeOrders.Dequeue();
    }

    public CheckoutTable GetReadyToOrderTable()
    {
        return _readyToOrderTables.Dequeue();
    }

}
