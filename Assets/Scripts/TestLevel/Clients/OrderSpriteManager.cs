using System.Collections.Generic;
using UnityEngine;

public class OrderSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite _lemonade;
    [SerializeField] private Sprite _hotdog;

    private Dictionary<MachinesType, Sprite> _orderSpriteMap;

    private void Awake()
    {
        _orderSpriteMap = new Dictionary<MachinesType, Sprite>
        {
            { MachinesType.Lemonade, _lemonade },
            { MachinesType.HotDog, _hotdog }
        };
    }
    public Sprite GetOrderSprite(MachinesType orderType)
    {
        if (_orderSpriteMap.TryGetValue(orderType, out Sprite sprite))
        {
            return sprite;
        }

        return null;
    }
}
