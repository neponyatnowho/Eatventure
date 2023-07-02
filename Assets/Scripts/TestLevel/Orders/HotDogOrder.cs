public class HotDogOrder : IOrder
{
    private MachinesType _orderType = MachinesType.HotDog;
    public CheckoutTable OrderTable { get; set; }
    public MachinesType OrderType { get => _orderType; }

    public float Price { get; private set; }
    public OrderPrices OrderPrices => new OrderPrices();
    public HotDogOrder()
    {
        Price = OrderPrices.GetPrice(_orderType);
    }

}
