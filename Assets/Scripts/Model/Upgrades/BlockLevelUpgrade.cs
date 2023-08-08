using UnityEngine.Events;

[System.Serializable]
public class BlockLevelUpgrade : Upgrade
{
    public event UnityAction<double, int> BlockLevelUpgradePurchased;

    protected override void InvokeBuyEvent(double upgradePrice, int upgradeLevel) => 
        BlockLevelUpgradePurchased?.Invoke(upgradePrice, upgradeLevel);

}
