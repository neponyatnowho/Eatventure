using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    private List<MachinesType> _openMachinesType;

    public void SetOpenMachinesType(List<MachinesType> machineType)
    {
        _openMachinesType = machineType;
    }
    public Order GetRandomOrder()
    {
        if(_openMachinesType.Count == 0)
            return new Order(MachinesType.Lemonade);
        return new Order(GetRandomMachiteType());
    }

    private MachinesType GetRandomMachiteType()
    {
        return _openMachinesType[Random.Range(0, _openMachinesType.Count)];
    }
}
