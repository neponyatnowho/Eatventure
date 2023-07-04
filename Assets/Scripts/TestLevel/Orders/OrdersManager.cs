using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public IOrder GetRandomOrder()
    {
        var i = Random.Range(0, 2);
        if (i == 0)
        {
            return new LemonadeOrder();
        }
        else
        {
            return new HotDogOrder();
        }
    }
}
