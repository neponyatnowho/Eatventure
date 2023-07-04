using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MoneyText : MonoBehaviour
{
    [SerializeField] private MoneyController _moneyController;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _moneyController.OnMoneyChanged += ChangeMoneyText;
    }
    private void ChangeMoneyText(float money)
    {
        _text.text = NumbersFormatter.Format(money);
    }
}
