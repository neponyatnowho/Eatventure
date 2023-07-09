using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class OrdersInfo: MonoBehaviour
{
    public Action<int, MachinesType> OnLevelDoublePriceArrived;
    private static readonly string priceKeySuffix = "Price";
    private static readonly string timeKeySuffix = "Time";
    private static readonly string levelKeySuffix = "Level";
    private static readonly string upgradePriceKeySuffix = "UpgradePrice";

    private static readonly Dictionary<MachinesType, double> cachedPrices = new Dictionary<MachinesType, double>();
    private static readonly Dictionary<MachinesType, float> cachedTimes = new Dictionary<MachinesType, float>();
    private static readonly Dictionary<MachinesType, int> cachedLevels = new Dictionary<MachinesType, int>();
    private static readonly Dictionary<MachinesType, double> cachedUpgradePrice = new Dictionary<MachinesType, double>();

    public double GetPrice(MachinesType orderType)
    {
        if (cachedPrices.ContainsKey(orderType))
        {
            return cachedPrices[orderType];
        }
        else
        {
            string key = GetKey(orderType, priceKeySuffix);
            string priceText = PlayerPrefs.GetString(key, "1");
            double price = NumbersFormatter.Format(priceText);
            cachedPrices.Add(orderType, price);
            return price;
        }
    }
    public float GetTime(MachinesType orderType)
    {
        if (cachedTimes.ContainsKey(orderType))
        {
            return cachedTimes[orderType];
        }
        else
        {
            string key = GetKey(orderType, timeKeySuffix);
            float time = PlayerPrefs.GetFloat(key, 1f);
            cachedTimes.Add(orderType, time);
            return time;
        }
    }
    public int GetLevel(MachinesType orderType)
    {
        if (cachedLevels.ContainsKey(orderType))
        {
            return cachedLevels[orderType];
        }
        else
        {
            string key = GetKey(orderType, levelKeySuffix);
            int level = PlayerPrefs.GetInt(key, 0);
            cachedLevels.Add(orderType, level);
            return level;
        }
    }
    public double GetUpgradePrice(MachinesType orderType)
    {
        if (cachedUpgradePrice.ContainsKey(orderType))
        {
            return cachedUpgradePrice[orderType];
        }
        else
        {
            string key = GetKey(orderType, upgradePriceKeySuffix);
            string upgradePriceText = PlayerPrefs.GetString(key, "1");
            double upgradePrice = NumbersFormatter.Format(upgradePriceText);
            cachedUpgradePrice.Add(orderType, upgradePrice);
            return upgradePrice;
        }
    }

    public void UpgradeOrder(MachinesType orderType)
    {
        AddLevel(orderType);
        UpgradePrice(orderType);
        UpgradeTime(orderType);
        UpgradePriceToUpgrade(orderType);
    }
    public void SetPrice(MachinesType orderType, double price)
    {
        string key = GetKey(orderType, priceKeySuffix);
        string priceText = NumbersFormatter.Format(price);
        PlayerPrefs.SetString(key, priceText);

        if (cachedPrices.ContainsKey(orderType))
        {
            cachedPrices[orderType] = price;
        }
        else
        {
            cachedPrices.Add(orderType, price);
        }
    }

    public void SetTime(MachinesType orderType, float time)
    {
        string key = GetKey(orderType, timeKeySuffix);
        PlayerPrefs.SetFloat(key, time);

        if (cachedTimes.ContainsKey(orderType))
        {
            cachedTimes[orderType] = time;
        }
        else
        {
            cachedTimes.Add(orderType, time);
        }
    }

    private void AddLevel(MachinesType orderType)
    {
        string key = GetKey(orderType, levelKeySuffix);
        int nextLevel;
        if (cachedLevels.ContainsKey(orderType))
        {
            nextLevel = cachedLevels[orderType] + 1;
            cachedLevels[orderType] = nextLevel;
        }
        else
        {
            nextLevel = PlayerPrefs.GetInt(key, 0) + 1;
            cachedLevels.Add(orderType, nextLevel);
        }
        PlayerPrefs.SetInt(key, nextLevel);
    }
    public void SetUpgradePrice(MachinesType orderType, double upgradePrice)
    {
        string key = GetKey(orderType, upgradePriceKeySuffix);
        string priceText = NumbersFormatter.Format(upgradePrice);

        PlayerPrefs.SetString(key, priceText);

        if (cachedUpgradePrice.ContainsKey(orderType))
        {
            cachedUpgradePrice[orderType] = upgradePrice;
        }
        else
        {
            cachedUpgradePrice.Add(orderType, upgradePrice);
        }
    }

    private void UpgradePrice(MachinesType orderType)
    {
        double currentPrice = GetPrice(orderType);
        double upgradePrice;
        int currentLevel = GetLevel(orderType);

        if (Enum.IsDefined(typeof(DoublePriceLevel), currentLevel))
        {
            upgradePrice = currentPrice * 2;
            OnLevelDoublePriceArrived?.Invoke(currentLevel, orderType);
        }
        else
        {
            upgradePrice = currentPrice + 1;
        }

        SetPrice(orderType, upgradePrice);
    }

    private void UpgradeTime(MachinesType orderType)
    {
        float currentTime = GetTime(orderType);
        float upgradeTime = currentTime * 0.99f;

        SetTime(orderType, upgradeTime);
    }

    private void UpgradePriceToUpgrade(MachinesType orderType)
    {
        double currentUpgradePrice = GetUpgradePrice(orderType);
        double upgradePriceToUpgrade;

        if (currentUpgradePrice < 10)
        {
            upgradePriceToUpgrade = currentUpgradePrice + 1;

            SetUpgradePrice(orderType, upgradePriceToUpgrade);
            return;
        }
        upgradePriceToUpgrade = currentUpgradePrice * 1.2;


        SetUpgradePrice(orderType, upgradePriceToUpgrade);
    }



    private string GetKey(MachinesType orderType, string suffix)
    {
        return orderType.ToString() + suffix;
    }
}
