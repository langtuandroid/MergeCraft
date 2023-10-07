using UnityEngine;
using UnityEngine.UI;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private TranslatesContainer _translatesContainer;
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private BlockReplacer _blockReplacer;
    [SerializeField] private Notifications _notifications;
    [SerializeField] private UpgradesInfoShower _upgradesPricesShower;
    [SerializeField] private UpgradesDescriptionShower _upgradesDescriptionShower;
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Button _openShopButton;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _shopTouchBlockator;
    [Space(10), SerializeField] private BlockLevelUpgrade _blockLevelUpgrade;
    [SerializeField] private CreationSpeedUpgrade _creationSpeedUpgrade;
    [SerializeField] private BlockMoneyUpgrade _blockMoneyUpgrade;
    [SerializeField] private MoneyUpgrade _moneyUpgrade;

    private PanelAnimator _panelAnimator = new PanelAnimator();
    private Wallet _wallet;

    private bool BlockUpgradesButtonsDisabled =>
        _blockLevelUpgrade.CanBuyUpgrade == false && _creationSpeedUpgrade.CanBuyUpgrade == false;

    private bool MoneyUpgradesButtonsDisabled =>
        _blockMoneyUpgrade.CanBuyUpgrade == false && _moneyUpgrade.CanBuyUpgrade == false;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _moneyUpgrade.Initialize(_wallet);
        _blockLevelUpgrade.Initialize(_wallet);
        _creationSpeedUpgrade.Initialize(_wallet);
        _blockMoneyUpgrade.Initialize(_wallet);
    }

    private void OnCreationSpeedUpgradePurchased() => _blockCreator.TryDecreaseCreationDuration();
    private void OnMoneyUpgradePurchased() => _wallet.TryIncreaseMoneyMultiplier();
    private void OnBlockMoneyUpgradePurchased() => _wallet.TryIncreaseAdditionalBlockMoney();

    private void OnBlockUpgradeLevelChanged(double upgradePrice, int upgradeLevel) => 
        _upgradesPricesShower.ShowBlockLevelInfo(upgradePrice, upgradeLevel);

    private void OnCreationSpeedUpgradeLevelChanged(double upgradePrice, int upgradeLevel) => 
        _upgradesPricesShower.ShowCreationSpeedInfo(upgradePrice, upgradeLevel);

    private void OnMoneyUpgradeLevelChanged(double upgradePrice, int upgradeLevel) => 
        _upgradesPricesShower.ShowMoneyMultiplierInfo(upgradePrice, upgradeLevel);

    private void OnBlockMoneyUpgradeLevelChanged(double upgradePrice, int upgradeLevel) => 
        _upgradesPricesShower.ShowBlockMoneyMultiplierInfo(upgradePrice, upgradeLevel);

    private void OnBlockLevelUpgradePurchased()
    {
        _blockCreator.TryIncreaseBlockLevel();
        _blockReplacer.TryReplaceBlocks(_blockCreator.CreationBlockLevel);
    }

    private void OnTranslateSelected()
    {
        _upgradesDescriptionShower.Show(_translatesContainer.SelectedTranslate);
        _openShopButton.interactable = true;
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

    private void Update()
    {
        TryActivateBuyButton(_blockLevelUpgrade);
        TryActivateBuyButton(_creationSpeedUpgrade);
        TryActivateBuyButton(_moneyUpgrade);
        TryActivateBuyButton(_blockMoneyUpgrade);

        if (BlockUpgradesButtonsDisabled && MoneyUpgradesButtonsDisabled)
            _notifications.DeactivateShopNotification();
    }

    private void OnEnable()
    {
        _openShopButton.interactable = false;
        _translatesContainer.TranslateSelected += OnTranslateSelected;

        _blockLevelUpgrade.BlockLevelUpgradePurchased += OnBlockLevelUpgradePurchased;
        _creationSpeedUpgrade.CreationSpeedUpgradePurchased += OnCreationSpeedUpgradePurchased;
        _moneyUpgrade.MoneyUpgradePurchased += OnMoneyUpgradePurchased;
        _blockMoneyUpgrade.BlockMoneyUpgradePurchased += OnBlockMoneyUpgradePurchased;

        _blockLevelUpgrade.BlockUpgradeLevelChanged += OnBlockUpgradeLevelChanged;
        _creationSpeedUpgrade.CreationSpeedUpgradeLevelChanged += OnCreationSpeedUpgradeLevelChanged;
        _moneyUpgrade.MoneyUpgradeLevelChanged += OnMoneyUpgradeLevelChanged;
        _blockMoneyUpgrade.BlockMoneyUpgradeLevelChanged += OnBlockMoneyUpgradeLevelChanged;

        _closeShopButton.onClick.AddListener(() => _shopTouchBlockator.SetActive(false));
        _openShopButton.onClick.AddListener(() => _shopTouchBlockator.SetActive(true));
        _openShopButton.onClick.AddListener(() => _panelAnimator.LaunchIncreaseAnimation(_shopPanel));
    }

    private void OnDisable()
    {
        _translatesContainer.TranslateSelected -= OnTranslateSelected;

        _blockLevelUpgrade.BlockLevelUpgradePurchased -= OnBlockLevelUpgradePurchased;
        _creationSpeedUpgrade.CreationSpeedUpgradePurchased -= OnCreationSpeedUpgradePurchased;
        _moneyUpgrade.MoneyUpgradePurchased -= OnMoneyUpgradePurchased;
        _blockMoneyUpgrade.BlockMoneyUpgradePurchased -= OnBlockMoneyUpgradePurchased;

        _blockLevelUpgrade.BlockUpgradeLevelChanged -= OnBlockUpgradeLevelChanged;
        _creationSpeedUpgrade.CreationSpeedUpgradeLevelChanged -= OnCreationSpeedUpgradeLevelChanged;
        _moneyUpgrade.MoneyUpgradeLevelChanged -= OnMoneyUpgradeLevelChanged;
        _blockMoneyUpgrade.BlockMoneyUpgradeLevelChanged -= OnBlockMoneyUpgradeLevelChanged;

        _blockLevelUpgrade.RemoveBuyButtonListeners();
        _creationSpeedUpgrade.RemoveBuyButtonListeners();
        _moneyUpgrade.RemoveBuyButtonListeners();
        _blockMoneyUpgrade.RemoveBuyButtonListeners();

        _openShopButton.onClick.RemoveAllListeners();
        _closeShopButton.onClick.RemoveAllListeners();
    }
}
