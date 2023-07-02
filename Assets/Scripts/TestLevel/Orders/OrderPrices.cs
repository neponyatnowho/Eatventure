using System.Collections.Generic;

public class OrderPrices
{
    private Dictionary<MachinesType, float> prices;

    public OrderPrices()
    {
        prices = new Dictionary<MachinesType, float>();
        prices[MachinesType.Lemonade] = 1f;
        prices[MachinesType.HotDog] = 5f;
    }

    public float GetPrice(MachinesType orderType)
    {
        if (prices.ContainsKey(orderType))
            return prices[orderType];
        else
            return 0f;
    }

    public void SetPrice(MachinesType orderType, float price)
    {
        if (prices.ContainsKey(orderType))
            prices[orderType] = price;
        else
            prices.Add(orderType, price);
    }
}
