using UnityEngine.Events;

[System.Serializable]
public class CreationSpeedUpgrade : Upgrade
{
    public event UnityAction CreationSpeedUpgradePurchased;
    public event UnityAction<double, int> CreationSpeedUpgradeLevelChanged;

    protected override void InvokeBuyEvent() => CreationSpeedUpgradePurchased?.Invoke();

    protected override void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel) => 
        CreationSpeedUpgradeLevelChanged?.Invoke(upgradePrice, upgradeLevel);
}
