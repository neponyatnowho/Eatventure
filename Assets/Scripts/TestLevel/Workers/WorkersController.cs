using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class WorkersController : MonoBehaviour
{
    public event Action OnCheckoutComplete;
    public event Action OnOrderComplete;

    [SerializeField] private List<Worker> _workers;
    private List<ComplexMachineTables> _machineTables = new List<ComplexMachineTables>();
    private Queue<Order> _orders = new Queue<Order>();


    private void Awake()
    {
        foreach (var worker in _workers)
        {
            worker.OnCheckoutComplete += OnCheckoutComplete;
            worker.OnOrderComplete += OnOrderComplete;
            worker.OnCookingComplete += GiveWorkForWorkers;
        }
    }

    public void AddMachineList(List<ComplexMachineTables> machines)
    {
        foreach (var machine in machines)
        {
            _machineTables.Add(machine);
        }
    }

    public void CheckoutTable(CheckoutTable table)
    {
        Worker currentWorker = GetFreeWorker();
        currentWorker.TakeOrder(table);
    }

    public void AddOrderForWorkers(Order order)
    {
        _orders.Enqueue(order);
        GiveWorkForWorkers();
    }
    public bool IsAnyWorkersFree()
    {
        return _workers.Any(worker => worker.IsFree);
    }

    private void GiveWorkForWorkers()
    {
        if (IsAnyWorkersFree() && _orders.Count != 0)
        {
            Debug.Log($"IsAnyMachineFree {IsAnyMachineFree(GetOrderType())}");

            if (IsAnyMachineFree(GetOrderType()))
            {
                Worker currentWorker = GetFreeWorker();
                Order currentOrder = GetOrder();
                ComplexMachineTables machineTable = GetWorkPointOnMachine(currentOrder.OrderType);

                UnitPoint<Worker> workPoint = machineTable.GetFreePoint();
                float timeToCook = machineTable.GetCookingTime();

                workPoint.Reserv(currentWorker);
                currentWorker.MakeOrder(workPoint, currentOrder, timeToCook);
            }
        } 
    }
    private bool IsAnyMachineFree(MachinesType type)
    {
        return _machineTables.Any(machine => machine.MachineType == type && machine.IsFree());
    }

    private ComplexMachineTables GetWorkPointOnMachine (MachinesType type)
    {
        return  _machineTables.First(machine => machine.MachineType == type && machine.IsFree());
    }

    private Worker GetFreeWorker()
    {
        return _workers.First(worker => worker.IsFree);
    }

    private MachinesType GetOrderType()
    {
        return _orders.Peek().OrderType;
    }

    private Order GetOrder()
    {
        return _orders.Dequeue();
    }
}
