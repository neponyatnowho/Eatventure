using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TableExample", order = 54)]
public class TableExample : ScriptableObject
{
    [SerializeField] public MachinesType MachineType;
    [SerializeField] public Material IconMeterial;
    [SerializeField] public double StartPrice;
    [SerializeField] public double StartUpgradePrice;
    [SerializeField] public float StartTimeToCreate;
    [SerializeField] public WorkingMachine WorkingMachineObject;

}
