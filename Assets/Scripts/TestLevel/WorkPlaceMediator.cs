using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkPlaceMediator : MonoBehaviour
{
    [SerializeField] private ClientCheckout _clientCheckout;
    [SerializeField] private WorkersController _workerController;
    [SerializeField] private MachineTableController _machineTableController;
    [SerializeField] private OrdersManager _orderMenager;

    private List<ComplexMachineTables> _machineTables;
    private List<MachinesType> _openMachinesType;
    private int _readyToOrderTablesCount => _clientCheckout.ReadyToOrderTables.Count;
    private int _readyToMakeOrdersCount => _clientCheckout.ReadyToMakeOrders.Count;
    private void Awake()
    {
        _machineTables = _machineTableController.MachineTables.ToList();
        _workerController.AddMachineList(_machineTables);
        _clientCheckout.OnReadyTableAdded += TryFindWorkForWorkers;
        _workerController.OnCheckoutComplete += TryFindWorkForWorkers;
        _workerController.OnOrderComplete += TryFindWorkForWorkers;
        _machineTableController.OnTableOpen += OnNewMachineOpen;
        GetAllOpenMachineTypes();
        _orderMenager.SetOpenMachinesType(_openMachinesType);

    }

    private void TryFindWorkForWorkers()
    {
        if (_workerController.IsAnyWorkersFree())
        {
            if (_readyToOrderTablesCount > 0)
            {
                CheckoutTable processedTable = _clientCheckout.GetReadyToOrderTable();
                _workerController.CheckoutTable(processedTable);
                return;
            }

            if (_readyToMakeOrdersCount > 0)
            {
                Order processedOrder = _clientCheckout.GetReadyToMakeOrder();
                _workerController.AddOrderForWorkers(processedOrder);
                return;
            }
        }
    }
    private void OnNewMachineOpen(MachinesType type)
    {
        _openMachinesType.Add(type);
        TryFindWorkForWorkers();
    }

    public void GetAllOpenMachineTypes()
    {
        _openMachinesType =  _machineTables
            .Where(machine => machine.IsWorkingTableOpen)
            .Select(machine => machine.MachineType)
            .ToList();
    }

    private void OnDestroy()
    {
        _clientCheckout.OnReadyTableAdded -= TryFindWorkForWorkers;
        _workerController.OnCheckoutComplete -= TryFindWorkForWorkers;
        _workerController.OnOrderComplete -= TryFindWorkForWorkers;
        _machineTableController.OnTableOpen -= OnNewMachineOpen;
    }
}
