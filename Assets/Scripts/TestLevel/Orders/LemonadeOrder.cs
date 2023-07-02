public class LemonadeOrder : IOrder
{
    private MachinesType _orderType = MachinesType.Lemonade;
    public CheckoutTable OrderTable { get; set; }
    public MachinesType OrderType { get => _orderType; }

    public float Price { get; private set; }

    public OrderPrices OrderPrices => new OrderPrices();

    public LemonadeOrder()
    {
        Price = OrderPrices.GetPrice(_orderType);
    }

}
