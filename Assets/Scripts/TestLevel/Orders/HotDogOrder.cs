public class HotDogOrder
{
    private MachinesType _orderType = MachinesType.HotDog;
    public CheckoutTable OrderTable { get; set; }
    public MachinesType OrderType { get => _orderType; }

}
