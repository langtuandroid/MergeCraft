using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    public Wallet Wallet => _wallet;
    public BlockLevelUpgrade BlockLevelUpgrade => _blockLevelUpgrade;
    public CreationSpeedUpgrade CreationSpeedUpgrade => _creationSpeedUpgrade;
    public BlockMoneyUpgrade BlockMoneyUpgrade => _blockMoneyUpgrade;
    public MoneyUpgrade MoneyUpgrade => _moneyUpgrade;

    [SerializeField] private Notifications _notifications;
    [Space(10), SerializeField] private BlockLevelUpgrade _blockLevelUpgrade;
    [SerializeField] private CreationSpeedUpgrade _creationSpeedUpgrade;
    [SerializeField] private BlockMoneyUpgrade _blockMoneyUpgrade;
    [SerializeField] private MoneyUpgrade _moneyUpgrade;

    private Wallet _wallet;

    private bool BlockUpgradesButtonsDisabled => 
        _blockLevelUpgrade.CanBuyUpgrade == false && _creationSpeedUpgrade.CanBuyUpgrade == false;

    private bool MoneyUpgradesButtonsDisabled =>
        _blockMoneyUpgrade.CanBuyUpgrade == false && _moneyUpgrade.CanBuyUpgrade == false;

    private bool BlockUpgradesInitialized => 
        _blockLevelUpgrade.WalletInitialized == true && _creationSpeedUpgrade.WalletInitialized == true;

    private bool MoneyUpgradesInitialized => 
        _blockMoneyUpgrade.WalletInitialized == true && _moneyUpgrade.WalletInitialized == true;

    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryActivateBuyButtons()
    {
        if (BlockUpgradesInitialized && MoneyUpgradesInitialized)
        {
            TryActivateBuyButton(_blockLevelUpgrade);
            TryActivateBuyButton(_creationSpeedUpgrade);
            TryActivateBuyButton(_moneyUpgrade);
            TryActivateBuyButton(_blockMoneyUpgrade);

            if (BlockUpgradesButtonsDisabled && MoneyUpgradesButtonsDisabled)
                _notifications.DeactivateShopNotification();
        }
    }

    public void TryRecoverUpgrades()
    {
        _moneyUpgrade.TryRecoverUpgrade();
        _blockLevelUpgrade.TryRecoverUpgrade();
        _blockMoneyUpgrade.TryRecoverUpgrade();
        _creationSpeedUpgrade.TryRecoverUpgrade();

        _moneyUpgrade.Initialize(_wallet);
        _blockLevelUpgrade.Initialize(_wallet);
        _creationSpeedUpgrade.Initialize(_wallet);
        _blockMoneyUpgrade.Initialize(_wallet);
    }

    private void TryActivateBuyButton(Upgrade upgrade)
    {
        if (upgrade.CanBuyUpgrade)
        {
            upgrade.BuyUpgradeButton.interactable = true;
            _notifications.ActivateShopNotification();
        }
        else
        {
            upgrade.BuyUpgradeButton.interactable = false;
        }
    }
}
