using UnityEngine.Events;
using YG;

[System.Serializable]
public class MoneyUpgrade : Upgrade
{
    public event UnityAction MoneyUpgradePurchased;
    public event UnityAction<double, int> MoneyUpgradeLevelChanged;

    protected override int GetRestoredLevel() => YandexGame.savesData.MoneyUpgradeLevel;
    protected override void InvokeBuyEvent() => MoneyUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        MoneyUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);

    protected override void SaveUpgradeLevel(int level)
    {
        YandexGame.savesData.MoneyUpgradeLevel = level;
        YandexGame.SaveProgress();
    }
}
