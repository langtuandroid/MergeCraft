using UnityEngine.Events;

[System.Serializable]
public class MoneyUpgrade : Upgrade
{
    public event UnityAction MoneyUpgradePurchased;
    public event UnityAction<double, int> MoneyUpgradeLevelChanged;

    protected override void InvokeBuyEvent() => MoneyUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        MoneyUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);
}
