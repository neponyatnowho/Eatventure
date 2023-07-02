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
    private List<MachineTable> _machineTables = new List<MachineTable>();
    private Queue<IOrder> _orders = new Queue<IOrder>();


    private void Awake()
    {
        foreach (var worker in _workers)
        {
            worker.OnCheckoutComplete += OnCheckoutComplete;
            worker.OnOrderComplete += OnOrderComplete;
            worker.OnCookingComplete += GiveWorkForWorkers;
        }
    }

    public void AddMachineList(List<MachineTable> machines)
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

    public void AddOrderForWorkers(IOrder order)
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
            if (IsAnyMachineFree(GetOrderType()))
            {
                Worker currentWorker = GetFreeWorker();
                IOrder currentOrder = GetOrder();
                UnitPoint<Worker> workPoint = GetWorkPointOnMachine(currentOrder.OrderType);
                workPoint.Reserv(currentWorker);
                currentWorker.MakeOrder(workPoint, currentOrder);
            }
        }
    }
    private bool IsAnyMachineFree(MachinesType type)
    {
        return _machineTables.Any(machine => machine.MachineType == type && machine.IsFree());
    }

    private UnitPoint<Worker> GetWorkPointOnMachine (MachinesType type)
    {
        MachineTable machineTable =  _machineTables.First(machine => machine.MachineType == type && machine.IsFree());
        return machineTable.GetFreePoint();
    }

    private Worker GetFreeWorker()
    {
        return _workers.First(worker => worker.IsFree);
    }

    private MachinesType GetOrderType()
    {
        return _orders.Peek().OrderType;
    }

    private IOrder GetOrder()
    {
        return _orders.Dequeue();
    }
}
