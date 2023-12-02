using UnityEngine.Events;
using YG;

[System.Serializable]
public class BlockMoneyUpgrade : Upgrade
{
    public event UnityAction BlockMoneyUpgradePurchased;
    public event UnityAction<double, int> BlockMoneyUpgradeLevelChanged;

    protected override int GetRestoredLevel() => YandexGame.savesData.BlockMoneyUpgradeLevel;
    protected override void InvokeBuyEvent() => BlockMoneyUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        BlockMoneyUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);

    protected override void SaveUpgradeLevel(int level)
    {
        YandexGame.savesData.BlockMoneyUpgradeLevel = level;
        YandexGame.SaveProgress();
    }
}
