using System;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public Action<float> OnMoneyChanged;

    private float _money;

    private void Awake()
    {
        OnMoneyChanged?.Invoke(_money);
    }
    public void CashingTheOrder(IOrder order)
    {
        _money += order.Price;
        OnMoneyChanged?.Invoke(_money);
    }
}
