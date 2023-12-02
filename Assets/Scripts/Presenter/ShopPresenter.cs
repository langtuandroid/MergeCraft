using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private TranslatesContainer _translatesContainer;
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private BlockReplacer _blockReplacer;
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private Notifications _notifications;
    [SerializeField] private UpgradesInfoShower _upgradesPricesShower;
    [SerializeField] private UpgradesDescriptionShower _upgradesDescriptionShower;
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Button _openShopButton;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _shopTouchBlockator;
    [SerializeField] private Shop _shop;

    private PanelAnimator _panelAnimator = new PanelAnimator();

    private void OnCreationSpeedUpgradePurchased() => _blockCreator.TryDecreaseCreationDuration();
    private void OnMoneyUpgradePurchased() => _shop.Wallet.TryIncreaseMoneyMultiplier();
    private void OnBlockMoneyUpgradePurchased() => _shop.Wallet.TryIncreaseAdditionalBlockMoney();

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
        _buildingCreator.TryCreateBuilding();
    }

    private void OnTranslateSelected()
    {
        _upgradesDescriptionShower.Show(_translatesContainer.SelectedTranslate);
        _openShopButton.interactable = true;
    }

    private void OnEnable()
    {
        _openShopButton.interactable = false;
        _translatesContainer.TranslateSelected += OnTranslateSelected;

        _shop.BlockLevelUpgrade.BlockLevelUpgradePurchased += OnBlockLevelUpgradePurchased;
        _shop.CreationSpeedUpgrade.CreationSpeedUpgradePurchased += OnCreationSpeedUpgradePurchased;
        _shop.MoneyUpgrade.MoneyUpgradePurchased += OnMoneyUpgradePurchased;
        _shop.BlockMoneyUpgrade.BlockMoneyUpgradePurchased += OnBlockMoneyUpgradePurchased;

        _shop.BlockLevelUpgrade.BlockUpgradeLevelChanged += OnBlockUpgradeLevelChanged;
        _shop.CreationSpeedUpgrade.CreationSpeedUpgradeLevelChanged += OnCreationSpeedUpgradeLevelChanged;
        _shop.MoneyUpgrade.MoneyUpgradeLevelChanged += OnMoneyUpgradeLevelChanged;
        _shop.BlockMoneyUpgrade.BlockMoneyUpgradeLevelChanged += OnBlockMoneyUpgradeLevelChanged;

        _closeShopButton.onClick.AddListener(() => _shopTouchBlockator.SetActive(false));
        _openShopButton.onClick.AddListener(() => _shopTouchBlockator.SetActive(true));
        _openShopButton.onClick.AddListener(() => _panelAnimator.LaunchIncreaseAnimation(_shopPanel));

        _openShopButton.onClick.AddListener(() => YandexGame.FullscreenShow());
        _closeShopButton.onClick.AddListener(() => YandexGame.FullscreenShow());
    }

    private void OnDisable()
    {
        _translatesContainer.TranslateSelected -= OnTranslateSelected;

        _shop.BlockLevelUpgrade.BlockLevelUpgradePurchased -= OnBlockLevelUpgradePurchased;
        _shop.CreationSpeedUpgrade.CreationSpeedUpgradePurchased -= OnCreationSpeedUpgradePurchased;
        _shop.MoneyUpgrade.MoneyUpgradePurchased -= OnMoneyUpgradePurchased;
        _shop.BlockMoneyUpgrade.BlockMoneyUpgradePurchased -= OnBlockMoneyUpgradePurchased;

        _shop.BlockLevelUpgrade.BlockUpgradeLevelChanged -= OnBlockUpgradeLevelChanged;
        _shop.CreationSpeedUpgrade.CreationSpeedUpgradeLevelChanged -= OnCreationSpeedUpgradeLevelChanged;
        _shop.MoneyUpgrade.MoneyUpgradeLevelChanged -= OnMoneyUpgradeLevelChanged;
        _shop.BlockMoneyUpgrade.BlockMoneyUpgradeLevelChanged -= OnBlockMoneyUpgradeLevelChanged;

        _shop.BlockLevelUpgrade.RemoveBuyButtonListeners();
        _shop.CreationSpeedUpgrade.RemoveBuyButtonListeners();
        _shop.MoneyUpgrade.RemoveBuyButtonListeners();
        _shop.BlockMoneyUpgrade.RemoveBuyButtonListeners();

        _openShopButton.onClick.RemoveAllListeners();
        _closeShopButton.onClick.RemoveAllListeners();
    }

    private void Update() => _shop.TryActivateBuyButtons();
}
