
using UnityEngine;

public class MachineTable : MonoBehaviour
{
    [SerializeField] private Transform _workMachineContainer;
    [SerializeField] private WorkPoint _workPoint;
    
    private bool _isOpen;
    public Transform WorkMachineContainer => _workMachineContainer;
    public WorkPoint WorkPoint => _workPoint;
    public bool IsOpen => _isOpen;

    public void OpenTable()
    {
        gameObject.SetActive(true);
        _isOpen = true;
    }

}
