public interface IOrder
{
    OrderPrices OrderPrices {get;}
    MachinesType OrderType { get; }
    float Price { get; }
    CheckoutTable OrderTable { get; set; }
}