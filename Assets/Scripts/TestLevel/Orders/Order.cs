public class Order
{
    public MachinesType OrderType { get; }
    public CheckoutTable OrderTable { get; set; }

    public Order(MachinesType type)
    {
        OrderType = type;
    }

}