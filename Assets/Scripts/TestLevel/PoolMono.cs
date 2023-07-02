using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolMono<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] private T _prefub;
    [SerializeField] private int _startSize;

    private Transform _container => this.transform;
    protected List<T> Objects = new List<T>();

    private void Awake()
        => CreatePool(_startSize);

    private void CreatePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var createdObj = Instantiate(_prefub, _container);
            Objects.Add(createdObj);
            createdObj.gameObject.SetActive(false);
        }
    }

    public T GetFreeElement()
    {
        if(IsAnyFreeObjects())
        {
            var obj = Objects.First(obj => obj.gameObject.activeSelf == false);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        { 
            Debug.Log($"No elements in Pool");
            return null;
        }

    }
    private bool IsAnyFreeObjects()
    {
        return Objects.Any(obj => obj.gameObject.activeSelf == false);
    }
}
