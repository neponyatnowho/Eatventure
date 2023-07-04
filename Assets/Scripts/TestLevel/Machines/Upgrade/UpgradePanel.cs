using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UpgradePanel : MonoBehaviour
{

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _machineTypeText;
    [SerializeField] private TMP_Text _orderPriceText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _upgradePriceText;
    [SerializeField] private Button _upgradeButton;

    private MachinesType _machineType;
    private OrdersInfo _ordersInfo;
    private MoneyController _moneyController;
    private float _upgradePrice;

    private Canvas _canvas;

    public void Init(MachinesType machineType, OrdersInfo ordersInfo, MoneyController moneyController)
    {
        _moneyController = moneyController;
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        _ordersInfo = ordersInfo;
        _machineType = machineType;
        _moneyController.OnMoneyChanged += SetButtonInteractable;
        _canvas = GetComponent<Canvas>();
        UpdateAllTextInfo();

    }
    private void UpdateAllTextInfo()
    {
        _levelText.text = "Level " + _ordersInfo.GetLevel(_machineType).ToString();

        _machineTypeText.text = _machineType.ToString();

        _orderPriceText.text = NumbersFormatter.Format(_ordersInfo.GetPrice(_machineType));

        _timeText.text = NumbersFormatter.Format(_ordersInfo.GetTime(_machineType)) + "s";

        _upgradePrice = _ordersInfo.GetUpgradePrice(_machineType);
        _upgradePriceText.text = NumbersFormatter.Format(_upgradePrice);
    }

    private void OnUpgradeButtonClick()
    {
        if(_moneyController.TryBuyUpgradeToOrder(_machineType))
        {
            _ordersInfo.UpgradeOrder(_machineType);
            UpdateAllTextInfo();
        }

    }

    private void SetButtonInteractable(float money)
    {
        bool isInteractable = _upgradePrice <= money;
        _upgradeButton.interactable = isInteractable;
    }

    public void OpenPanel()
       => _canvas.enabled = true;
    public void ClosePanel()
        =>_canvas.enabled = false;
    
}
