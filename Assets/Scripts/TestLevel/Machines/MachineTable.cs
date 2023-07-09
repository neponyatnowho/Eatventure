
using UnityEngine;

public class MachineTable : MonoBehaviour
{
    [SerializeField] private Transform _workMachineContainer;
    [SerializeField] private WorkPoint _workPoint;

    public Transform WorkMachineContainer => _workMachineContainer;
    public WorkPoint WorkPoint => _workPoint;
}
