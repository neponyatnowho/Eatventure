using UnityEngine;
public class ClientsPool : PoolMono<Client>
{
    public void SetEndPointsToClients(Vector3 endPoint)
    {
        foreach (var clients in Objects)
        {
            clients.Init(endPoint);
        }
    }
}
