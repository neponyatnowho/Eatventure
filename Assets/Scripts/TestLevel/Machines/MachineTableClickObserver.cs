using System;
using UnityEngine;

public class MachineTableClickObserver : MonoBehaviour
{
    public event Action<MachineTable> OnTableClick;
    public event Action OnNonTableClick;

    [SerializeField] private TouchInput _touchInput;

    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
        _touchInput.OnClick += OnPlayerClick;
    }

    private void OnPlayerClick(Vector2 touchPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out MachineTable machineTable))
            OnTableClick?.Invoke(machineTable);
            else
                ProcessNonTableClick();
        }
        else
            ProcessNonTableClick();
    }

    private void ProcessNonTableClick()
    {
        OnNonTableClick?.Invoke();
    }
}
