using UnityEngine.Events;
using YG;

[System.Serializable]
public class BlockLevelUpgrade : Upgrade
{
    public event UnityAction BlockLevelUpgradePurchased;
    public event UnityAction<double, int> BlockUpgradeLevelChanged;

    protected override int GetRestoredLevel() => YandexGame.savesData.BlockUpgradeLevel;
    protected override void InvokeBuyEvent() => BlockLevelUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        BlockUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);

    protected override void SaveUpgradeLevel(int level)
    {
        YandexGame.savesData.BlockUpgradeLevel = level;
        YandexGame.SaveProgress();
    }
}
