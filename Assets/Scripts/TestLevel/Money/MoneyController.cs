using System;
using System.Collections;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private  OrdersInfo _ordersInfo;
    [SerializeField] private float _saveMoneyFrequency = 10f;
    public Action<float> OnMoneyChanged;

    private float _money;
    public float Money => _money;

    private void Awake()
    {
        _money = PlayerPrefs.GetFloat("money", 0f);
        RegisterMoneyChanged();
        StartCoroutine(SaveMoneyRoutine());
    }
    public void CashingTheOrder(IOrder order)
    {
        float orderPrice = _ordersInfo.GetPrice(order.OrderType);
        _money += orderPrice;
        RegisterMoneyChanged();
    }
    public bool TryBuyUpgradeToOrder(MachinesType orderType)
    {
        float upgradePrice = _ordersInfo.GetUpgradePrice(orderType);
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
            PlayerPrefs.SetFloat("money", _money);
            yield return new WaitForSecondsRealtime(_saveMoneyFrequency);
        }
    }
}
