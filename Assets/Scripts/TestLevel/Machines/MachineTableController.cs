using System.Collections.Generic;
using UnityEngine;

public class MachineTableController : MonoBehaviour
{
    [SerializeField] private List<ComplexMachineTables> _machineTables;
    [SerializeField] private MachineTableClickObserver _tableClickObserver;
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
        ComplexMachineTables tableToOpen = _machineTables.Find(table => table == machineTable);
        tableToOpen.OpenUpgradePanel();
        _currentTableWhithOpenPanel = tableToOpen;
    }

    private void CloseCurrentPanel()
    {
        if(_currentTableWhithOpenPanel != null)
            _currentTableWhithOpenPanel.CloseUpgradePanel();
    }
}
