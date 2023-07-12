using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UpgradePanel : MonoBehaviour
{
    public event Action OnNewTableLevel;

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _machineTypeText;
    [SerializeField] private TMP_Text _orderPriceText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _upgradePriceText;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Slider _sliderUpgradeProgress;

    private MachinesType _machineType;
    private OrdersInfo _ordersInfo;
    private MoneyController _moneyController;
    private double _upgradePrice;

    private Canvas _canvas;

    public void Init(MachinesType machineType, OrdersInfo ordersInfo, MoneyController moneyController)
    {
        _moneyController = moneyController;
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        _ordersInfo = ordersInfo;
        _ordersInfo.OnLevelDoublePriceArrived += OnDoubleLvlArrived;
        _machineType = machineType;
        _moneyController.OnMoneyChanged += SetButtonInteractable;
        _canvas = GetComponent<Canvas>();

        UpdateSliderInfo(_ordersInfo.GetLevel(_machineType));
        UpdateAllTextInfo();

    }

    private void OnDoubleLvlArrived(int currentDoubleLvl, MachinesType machineType)
    {
        if (machineType != _machineType)
            return;

        UpdateSliderInfo(currentDoubleLvl);
    }

    private void UpdateSliderInfo(int currentDoubleLvl)
    {
        DoublePriceLevel[] allLevels = (DoublePriceLevel[])Enum.GetValues(typeof(DoublePriceLevel));
        DoublePriceLevel nextLevel = DoublePriceLevel.Level10;

        foreach (DoublePriceLevel level in allLevels)
        {
            if ((int)level > currentDoubleLvl)
            {
                nextLevel = level;
                break;
            }
        }
        _sliderUpgradeProgress.minValue = currentDoubleLvl;

        if(nextLevel == DoublePriceLevel.Level10)
            _sliderUpgradeProgress.minValue = 0;

        _sliderUpgradeProgress.maxValue = (int)nextLevel;
    }
    private void UpdateAllTextInfo()
    {
        var level = _ordersInfo.GetLevel(_machineType);
        CheckNewTableLevel(level);
        _levelText.text = "Level " + level.ToString();

        _machineTypeText.text = _machineType.ToString();


        _sliderUpgradeProgress.value = level;

        _orderPriceText.text = NumbersFormatter.Format(_ordersInfo.GetPrice(_machineType));

        _timeText.text = NumbersFormatter.Format(_ordersInfo.GetTime(_machineType)) + "s";

        _upgradePrice = _ordersInfo.GetUpgradePrice(_machineType);
        _upgradePriceText.text = NumbersFormatter.Format(_upgradePrice);
    }

    private void CheckNewTableLevel(int level)
    {
        if (Enum.IsDefined(typeof(NewTableLevels), level))
            OnNewTableLevel?.Invoke();

    }

    private void OnUpgradeButtonClick()
    {
        if(_moneyController.TryBuyUpgradeToOrder(_machineType))
        {
            _ordersInfo.UpgradeOrder(_machineType);
            UpdateAllTextInfo();
        }

    }

    private void SetButtonInteractable(double money)
    {
        bool isInteractable = _upgradePrice < money;
        _upgradeButton.interactable = isInteractable;
    }

    public void OpenPanel()
       => _canvas.enabled = true;
    public void ClosePanel()
        =>_canvas.enabled = false;
    
}
