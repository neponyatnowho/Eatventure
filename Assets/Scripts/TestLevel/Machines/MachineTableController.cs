using System;
using System.Collections.Generic;
using UnityEngine;

public class MachineTableController : MonoBehaviour
{
    public event Action<MachinesType> OnTableOpen;
    [SerializeField] private List<ComplexMachineTables> _machineTables;
    [SerializeField] private ComplexMachineTableClickObserver _tableClickObserver;
    [SerializeField] private OrdersInfo _ordersInfo;
    [SerializeField] private MoneyController _moneyController;

    private ComplexMachineTables _currentTableWhithOpenPanel;
    public IEnumerable<ComplexMachineTables> MachineTables => _machineTables;

    private void Awake()
    {
        _tableClickObserver.OnTableClick += OnTableClicked;
        _tableClickObserver.OnNonTableClick += CloseCurrentPanel;

        foreach (var table in _machineTables)
        {
            table.Init(_ordersInfo, _moneyController);
            table.OnWorkinTableOpen += OnTableOpen;
        }
    }

    private void OnTableClicked(ComplexMachineTables machineTable)
    {
        if(_currentTableWhithOpenPanel != null && _currentTableWhithOpenPanel != machineTable)
        {
            CloseCurrentPanel();
        }
        OpenTableUpgradePanel(machineTable);
    }

    private void OpenTableUpgradePanel(ComplexMachineTables machineTable)
    {
        machineTable.OpenPanel();
        _currentTableWhithOpenPanel = machineTable;
    }

    private void CloseCurrentPanel()
    {
        if(_currentTableWhithOpenPanel != null)
            _currentTableWhithOpenPanel.ClosePanel();
    }

    private void OnDisable()
    {
        foreach (var table in _machineTables)
        {
            table.OnWorkinTableOpen -= OnTableOpen;
        }
    }
}
