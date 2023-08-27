using UnityEngine.Events;

[System.Serializable]
public class BlockMoneyUpgrade : Upgrade
{
    public event UnityAction BlockMoneyUpgradePurchased;
    public event UnityAction<double, int> BlockMoneyUpgradeLevelChanged;

    protected override void InvokeBuyEvent() => BlockMoneyUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        BlockMoneyUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);
}
