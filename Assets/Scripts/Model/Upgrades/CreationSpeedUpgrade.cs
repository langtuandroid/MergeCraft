using UnityEngine.Events;
using YG;

[System.Serializable]
public class CreationSpeedUpgrade : Upgrade
{
    public event UnityAction CreationSpeedUpgradePurchased;
    public event UnityAction<double, int> CreationSpeedUpgradeLevelChanged;

    protected override int GetRestoredLevel() => YandexGame.savesData.CreationSpeedUpgradeLevel;
    protected override void InvokeBuyEvent() => CreationSpeedUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        CreationSpeedUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);

    protected override void SaveUpgradeLevel(int level)
    {
        YandexGame.savesData.CreationSpeedUpgradeLevel = level;
        YandexGame.SaveProgress();
    }
}
