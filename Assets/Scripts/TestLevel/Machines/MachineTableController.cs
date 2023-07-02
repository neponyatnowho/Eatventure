using System.Collections.Generic;
using UnityEngine;

public class MachineTableController : MonoBehaviour
{
    [SerializeField] private List<MachineTable> _machineTables;
    [SerializeField] private MachineTableClickObserver _tableClickObserver;

    private MachineTable _currentTableWhithOpenPanel;
    public IEnumerable<MachineTable> MachineTables => _machineTables;

    private void Start()
    {
        _tableClickObserver.OnTableClick += OnTableClicked;
        _tableClickObserver.OnNonTableClick += CloseCurrentPanel;
    }

    private void OnTableClicked(MachineTable machineTable)
    {
        if(_currentTableWhithOpenPanel != null && _currentTableWhithOpenPanel != machineTable)
        {
            CloseCurrentPanel();
        }
        OpenTableUpgradePanel(machineTable);
    }

    private void OpenTableUpgradePanel(MachineTable machineTable)
    {
        MachineTable tableToOpen = _machineTables.Find(table => table == machineTable);
        tableToOpen.OpenUpgradePanel();
        _currentTableWhithOpenPanel = tableToOpen;
    }

    private void CloseCurrentPanel()
    {
        if(_currentTableWhithOpenPanel != null)
            _currentTableWhithOpenPanel.CloseUpgradePanel();
    }
}
