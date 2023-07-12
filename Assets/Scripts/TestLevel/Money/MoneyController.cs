using System;
using System.Collections;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private  OrdersInfo _ordersInfo;
    [SerializeField] private float _saveMoneyFrequency = 10f;
    public Action<double> OnMoneyChanged;

    private double _money;
    public double Money => _money;

    private void Awake()
    {
        var moneyText = PlayerPrefs.GetString("money", "5000");
        _money = NumbersFormatter.Format(moneyText);
        RegisterMoneyChanged();
        StartCoroutine(SaveMoneyRoutine());
    }
    public void CashingTheOrder(Order order)
    {
        double orderPrice = _ordersInfo.GetPrice(order.OrderType);
        _money += orderPrice;
        RegisterMoneyChanged();
    }
    public bool TryBuyUpgradeToOrder(MachinesType orderType)
    {
        double upgradePrice = _ordersInfo.GetUpgradePrice(orderType);
        if (upgradePrice < 0 || _money < upgradePrice)
            return false;

        _money -= upgradePrice;
        RegisterMoneyChanged();
        return true;
    }
    private void RegisterMoneyChanged()
    {
        OnMoneyChanged?.Invoke(_money);
    }
    
    private IEnumerator SaveMoneyRoutine()
    {
        while(true)
        {
            PlayerPrefs.SetString("money", NumbersFormatter.Format(_money));
            yield return new WaitForSecondsRealtime(_saveMoneyFrequency);
        }
    }
}
