using System;
using System.Collections;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private ClientsPool _clientsPool;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private IOrder[] _posibleOrders;
    [SerializeField] private OrdersManager _ordersManager;
    private void Start()
    {
        _clientsPool.SetEndPointsToClients(_endPoint.position);
        StartCoroutine(SpawningRoutine());
    }

    private IEnumerator SpawningRoutine()
    {
        while(true)
        {
            Client client = _clientsPool.GetFreeElement();
            if(client != null)
            {
                IOrder order = _ordersManager.GetRandomOrder();
                client.transform.position = _startPoint.position;
                client.SetOrder(order);
            }
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
