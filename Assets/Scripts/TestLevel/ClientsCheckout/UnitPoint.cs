using System;
using UnityEngine;

public abstract class UnitPoint<T> : MonoBehaviour where T : MonoBehaviour
{
    public event Action<UnitPoint<T>> OnReserved;
    protected T _currentUnit;
    protected bool _isFree = true;

    public bool IsFree => _isFree;
    public T CurrentUnit => _currentUnit;
    public Vector3 LookAtPoint
    {
        get
        {
            Vector3 localPositionWithForward = transform.localPosition + (Vector3.forward * 2);
            Vector3 worldPoint = transform.TransformPoint(localPositionWithForward);
            return worldPoint;
        }
    }

    public void Reserv(T unit)
    {
        _currentUnit = unit;
        _isFree = false;
        OnReserved?.Invoke(this);
    }

    public void SetFree()
    {
        _currentUnit = null;
        _isFree = true;
    }
}
