using System;
using UnityEngine;


public class TriggerObserverWithPayload<T> : MonoBehaviour
{
    public event Action<T> WhenTrigerEnter;
    public event Action<T> WhenTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T component))
            WhenTrigerEnter?.Invoke(component);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T component))
            WhenTriggerExit?.Invoke(component);
    }

}
