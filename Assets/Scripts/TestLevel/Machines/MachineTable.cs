using System.Linq;
using UnityEngine;

public class MachineTable : MonoBehaviour
{
    [SerializeField] private MachinesType _machineType;
    [SerializeField] private WorkPoint[] _workPoints;
    [SerializeField] private UpgradePanel _upgradePanel;

    public MachinesType MachineType => _machineType;

    public bool IsFree()
    {
        return _workPoints.Any(point => point.IsFree);
    }
    public WorkPoint GetFreePoint()
    {
        return _workPoints.First(point => point.IsFree);
    }

    public void OpenUpgradePanel()
    {
        _upgradePanel.OpenPanel();
    }

    public void CloseUpgradePanel()
    {
        _upgradePanel.ClosePanel();

    }
}
