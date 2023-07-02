using UnityEngine;

public class MoneyFxController : MonoBehaviour
{
    [SerializeField] private MoneyUIParticlePool _moneyUiParticle;
    [SerializeField] private MoneyParticlePool _moneyParticlePool;
    
    public void ShowFx(IOrder order)
    {
        var position = order.OrderTable.transform.position;

        position.y = 1.1f;

        var fx = _moneyParticlePool.GetFreeElement();
        fx.transform.position = position;
        var uiFx = _moneyUiParticle.GetFreeElement();
        uiFx.Show(order.Price, position);
    }
}
