using UnityEngine.Events;

[System.Serializable]
public class BlockLevelUpgrade : Upgrade
{
    public event UnityAction BlockLevelUpgradePurchased;
    public event UnityAction<double, int> BlockUpgradeLevelChanged;

    protected override void InvokeBuyEvent() => BlockLevelUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        BlockUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);
}
