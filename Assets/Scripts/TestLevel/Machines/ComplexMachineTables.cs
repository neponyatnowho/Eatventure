using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComplexMachineTables : MonoBehaviour
{
    public event Action<MachinesType> OnWorkinTableOpen;


    [SerializeField] private TableExample _tableInfo;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private UnlockPanel _unlockPanel;
    [SerializeField] private IconTable _iconsTable;
    [SerializeField] private MachineTable[] _machineTables;
    [SerializeField] private TableOpener _tableOpener;
    

    private WorkPoint[] _workPoints;
    private List<WorkPoint> _openWorkPoints = new List<WorkPoint>();
    private Transform[] _workingMachineContainers;
    private MachinesType _machineType;
    private MoneyController _moneyController;
    private OrdersInfo _ordersInfo;
    private int _startLvl;
    private bool _isWorkingTableOpen;
    public MachinesType MachineType => _machineType;
    public bool IsWorkingTableOpen => _isWorkingTableOpen;


    public void Init(OrdersInfo ordersInfo, MoneyController moneyController)
    {
        _machineType = _tableInfo.MachineType;
        _iconsTable.SetIcon(_tableInfo.IconMeterial);
        _ordersInfo = ordersInfo;
        _moneyController = moneyController;
        _startLvl = _ordersInfo.GetLevel(_tableInfo.MachineType);
        _upgradePanel.OnNewTableLevel += OpenNewWorkTable;
        InitMachineTables();
        InitOrderInfo();


        if (_startLvl == 0)
        {
            _isWorkingTableOpen = false;
            _unlockPanel.Init(moneyController, ordersInfo, _machineType);
            _unlockPanel.OnUnlock += OnTableUnlocked;
            _unlockPanel.OnUnlock += _tableOpener.DisableTable;
            _unlockPanel.OnInteractableChanged += _tableOpener.ActivateIndicator;
        }
        else
        {
            _isWorkingTableOpen = true;
            _upgradePanel.Init(_machineType, _ordersInfo, _moneyController);

            

            foreach (int levelValue in Enum.GetValues(typeof(NewTableLevels)))
            {
                if (_startLvl >= levelValue)
                {
                    OpenNewWorkTable();
                }
            }
        }

        if (_startLvl > 0)
        {
            OpenNewWorkTable();
        }

    }

    public bool IsFree()
    {
        return _openWorkPoints.Any(point => point.IsFree);
    }
    public WorkPoint GetFreePoint()
    {
        return _openWorkPoints.First(point => point.IsFree);
    }

    public float GetCookingTime()
    {
        return _ordersInfo.GetTime(_tableInfo.MachineType);
    }

    public void OpenPanel()
    {
        if (_isWorkingTableOpen)
            _upgradePanel.OpenPanel();
        else
            _unlockPanel.OpenPanel();
    }
    public void ClosePanel()
    {
        if (_isWorkingTableOpen)
            _upgradePanel.ClosePanel();
        else
            _unlockPanel.ClosePanel();
    }

    private void InitMachineTables()
    {

        _workPoints = new WorkPoint[_machineTables.Length];
        _workingMachineContainers = new Transform[_machineTables.Length];
        for (int i = 0; i < _machineTables.Length; i++)
        {
            _workPoints[i] = _machineTables[i].WorkPoint;
            _workingMachineContainers[i] = _machineTables[i].WorkMachineContainer;
            _machineTables[i].gameObject.SetActive(false);
        }

        foreach (var workingMachineContainer in _workingMachineContainers)
        {
            Instantiate(_tableInfo.WorkingMachineObject, workingMachineContainer);
        }

    }  

    private void OnTableUnlocked()
    {
        _isWorkingTableOpen = true;
        OnWorkinTableOpen?.Invoke(_machineType);
        OpenNewWorkTable();
        _upgradePanel.Init(_machineType, _ordersInfo, _moneyController);
        _unlockPanel.OnUnlock -= OnTableUnlocked;
    }

    private void OpenNewWorkTable()
    {
        MachineTable tableToOpen = _machineTables.FirstOrDefault(table => !table.IsOpen);
        if (tableToOpen == null)
            return;
        _openWorkPoints.Add(tableToOpen.WorkPoint);
        tableToOpen.OpenTable();
    }


    private void InitOrderInfo()
    {
        if (_ordersInfo.GetPrice(_tableInfo.MachineType) < _tableInfo.StartPrice)
            _ordersInfo.SetPrice(_machineType, _tableInfo.StartPrice);

        if (_ordersInfo.GetUpgradePrice(_tableInfo.MachineType) < _tableInfo.StartUpgradePrice)
            _ordersInfo.SetUpgradePrice(_machineType, _tableInfo.StartUpgradePrice);

        if (_ordersInfo.GetTime(_tableInfo.MachineType) < _tableInfo.StartTimeToCreate)
            _ordersInfo.SetTime(_machineType, _tableInfo.StartTimeToCreate);
    }
}
