using System.Collections.Generic;
using UnityEngine;

public class OrdersInfo: MonoBehaviour
{
    private static readonly string priceKeySuffix = "Price";
    private static readonly string timeKeySuffix = "Time";
    private static readonly string levelKeySuffix = "Level";
    private static readonly string upgradePriceKeySuffix = "Level";

    private static readonly Dictionary<MachinesType, float> cachedPrices = new Dictionary<MachinesType, float>();
    private static readonly Dictionary<MachinesType, float> cachedTimes = new Dictionary<MachinesType, float>();
    private static readonly Dictionary<MachinesType, int> cachedLevels = new Dictionary<MachinesType, int>();
    private static readonly Dictionary<MachinesType, float> cachedUpgradePrice = new Dictionary<MachinesType, float>();

    public float GetPrice(MachinesType orderType)
    {
        if (cachedPrices.ContainsKey(orderType))
        {
            return cachedPrices[orderType];
        }
        else
        {
            string key = GetKey(orderType, priceKeySuffix);
            float price = PlayerPrefs.GetFloat(key, 1f);
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
    public float GetUpgradePrice(MachinesType orderType)
    {
        if (cachedUpgradePrice.ContainsKey(orderType))
        {
            return cachedUpgradePrice[orderType];
        }
        else
        {
            string key = GetKey(orderType, upgradePriceKeySuffix);
            float upgradePrice = PlayerPrefs.GetFloat(key, 1f);
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
    private void SetPrice(MachinesType orderType, float price)
    {
        string key = GetKey(orderType, priceKeySuffix);
        PlayerPrefs.SetFloat(key, price);

        if (cachedPrices.ContainsKey(orderType))
        {
            cachedPrices[orderType] = price;
        }
        else
        {
            cachedPrices.Add(orderType, price);
        }
    }

    private void SetTime(MachinesType orderType, float time)
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
    private void SetUpgradePrice(MachinesType orderType, float upgradePrice)
    {
        string key = GetKey(orderType, upgradePriceKeySuffix);
        PlayerPrefs.SetFloat(key, upgradePrice);

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
        float currentPrice = GetPrice(orderType);
        float upgradePrice = currentPrice * 1.5f;

        SetPrice(orderType, upgradePrice);
    }

    private void UpgradeTime(MachinesType orderType)
    {
        float currentTime = GetTime(orderType);
        float upgradeTime = currentTime * 0.95f;

        SetTime(orderType, upgradeTime);
    }

    private void UpgradePriceToUpgrade(MachinesType orderType)
    {
        float currentUpgradePrice = GetUpgradePrice(orderType);
        float upgradePriceToUpgrade = currentUpgradePrice * 1.8f;

        SetUpgradePrice(orderType, upgradePriceToUpgrade);
    }



    private string GetKey(MachinesType orderType, string suffix)
    {
        return orderType.ToString() + suffix;
    }
}
