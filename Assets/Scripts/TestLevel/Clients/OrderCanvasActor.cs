using UnityEngine;
using UnityEngine.UI;

public class OrderCanvasActor : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private OrderSpriteManager _spriteManager;
    [SerializeField] private Image _iconSpriteRenderer;

    private void Awake()
    {
        HideOrder();
        transform.rotation = Camera.main.transform.rotation;
    }
    public void ShowOrder(MachinesType orderType)
    {
        Sprite orderIconSprite = _spriteManager.GetOrderSprite(orderType);
        if (orderIconSprite != null)
        {
            _iconSpriteRenderer.sprite = orderIconSprite;
        }
        _canvas.enabled = true;
    }
    public void HideOrder()
    {
        _canvas.enabled = false;
    }
}
