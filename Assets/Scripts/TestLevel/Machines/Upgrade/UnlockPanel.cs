using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel : MonoBehaviour
{
    public event Action OnUnlock;
    public event Action<bool> OnInteractableChanged;
    [SerializeField] private TMP_Text _unlockPriceText;
    [SerializeField] private Button _unlockButton;

    private MachinesType _machineType;
    private MoneyController _moneyController;
    private OrdersInfo _ordersInfo;
    private double _upgradePrice;

    private Canvas _canvas;


    public void Init(MoneyController moneyController, OrdersInfo ordersInfo, MachinesType machineType)
    {
        _unlockButton.onClick.AddListener(OnUpgradeButtonClick);

        _machineType = machineType;
        _ordersInfo = ordersInfo;
        _moneyController = moneyController;
        _canvas = GetComponent<Canvas>();
        _moneyController.OnMoneyChanged += SetButtonInteractable;
        _upgradePrice = _ordersInfo.GetUpgradePrice(_machineType);
        _unlockPriceText.text = NumbersFormatter.Format(_upgradePrice);
    }
    private void OnUpgradeButtonClick()
    {
        if (_moneyController.TryBuyUpgradeToOrder(_machineType))
        {
            _ordersInfo.AddLevel(_machineType);
            ClosePanel();
            OnUnlock?.Invoke();
        }
    }

    private void SetButtonInteractable(double money)
    {
        bool isInteractable = _upgradePrice < money;
        _unlockButton.interactable = isInteractable;
        OnInteractableChanged?.Invoke(isInteractable);
    }
    public void OpenPanel()
        => _canvas.enabled = true;
    public void ClosePanel()
        => _canvas.enabled = false;
}
