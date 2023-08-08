using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade
{
    public Button BuyUpgradeButton => _buyUpgradeButton;
    public bool CanBuyUpgrade => _wallet.Money >= UpgradePrice;

    [SerializeField] private Button _buyUpgradeButton;
    [Space(10), SerializeField] private double _startUpgradePrice;
    [SerializeField] private int _priceIncreaseMultiplier;
    [SerializeField] private int _maxUpgradeLevel;

    private int _upgradeLevel = 0;
    private Wallet _wallet;

    private double UpgradePrice => _startUpgradePrice * _upgradeLevel * _priceIncreaseMultiplier;

    public void RemoveBuyButtonListeners() =>
        _buyUpgradeButton.onClick.RemoveAllListeners();

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _buyUpgradeButton.onClick.AddListener(() => TryBuyUpgrade());
    }

    private void TryBuyUpgrade()
    {
        if (CanBuyUpgrade)
        {
            _wallet.TryDecreaseMoney(UpgradePrice);
            _upgradeLevel++;

            InvokeBuyEvent(UpgradePrice, _upgradeLevel);
        }
    }

    protected abstract void InvokeBuyEvent(double upgradePrice, int upgradeLevel);
}
