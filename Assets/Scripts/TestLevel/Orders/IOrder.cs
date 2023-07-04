public interface IOrder
{
    MachinesType OrderType { get; }
    CheckoutTable OrderTable { get; set; }
}