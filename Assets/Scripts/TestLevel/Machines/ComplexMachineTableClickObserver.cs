using System;
using UnityEngine;

public class ComplexMachineTableClickObserver : MonoBehaviour
{
    public event Action<ComplexMachineTables> OnTableClick;
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
            ComplexMachineTables table = hit.collider.GetComponentInParent<ComplexMachineTables>();

            if (table != null)
            { 
                OnTableClick?.Invoke(table);
                return;            
            }
        }

        ProcessNonTableClick();
    }

    private void ProcessNonTableClick()
    {
        OnNonTableClick?.Invoke();
    }
}
