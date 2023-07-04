public class LemonadeOrder : IOrder
{
    private MachinesType _orderType = MachinesType.Lemonade;
    public CheckoutTable OrderTable { get; set; }
    public MachinesType OrderType { get => _orderType; }

}
