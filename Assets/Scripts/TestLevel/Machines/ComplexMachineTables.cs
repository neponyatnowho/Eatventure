using System.Linq;
using UnityEngine;

public class ComplexMachineTables : MonoBehaviour
{
    [SerializeField] private TableExample _tableInfo;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private IconTable _iconsTable;
    [SerializeField] private MachineTable[] _machineTables;
    

    private WorkPoint[] _workPoints;
    private Transform[] _workingMachineContainers;
    private MachinesType _machineType;
    private OrdersInfo _ordersInfo;
    public MachinesType MachineType => _machineType;

    public void Init(OrdersInfo ordersInfo, MoneyController moneyController)
    {
        _machineType = _tableInfo.MachineType;
        _iconsTable.SetIcon(_tableInfo.IconMeterial);
        _ordersInfo = ordersInfo;

        InitMachineTables();
        InitOrderInfo();
        _upgradePanel.Init(_machineType, ordersInfo, moneyController);

    }

    public bool IsFree()
    {
        return _workPoints.Any(point => point.IsFree);
    }
    public WorkPoint GetFreePoint()
    {
        return _workPoints.First(point => point.IsFree);
    }

    public float GetCookingTime()
    {
        return _ordersInfo.GetTime(_tableInfo.MachineType);
    }

    public void OpenUpgradePanel() => _upgradePanel.OpenPanel();
    public void CloseUpgradePanel() => _upgradePanel.ClosePanel();

    private void InitMachineTables()
    {
        _workPoints = new WorkPoint[_machineTables.Length];
        _workingMachineContainers = new Transform[_machineTables.Length];
        for (int i = 0; i < _machineTables.Length; i++)
        {
            _workPoints[i] = _machineTables[i].WorkPoint;
            _workingMachineContainers[i] = _machineTables[i].WorkMachineContainer;
        }

        foreach (var workingMachineContainer in _workingMachineContainers)
        {
            Instantiate(_tableInfo.WorkingMachineObject, workingMachineContainer);
        }
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
