using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade
{
    public Button BuyUpgradeButton => _buyUpgradeButton;
    public bool CanBuyUpgrade => _wallet.Money >= _upgradePrice;

    [SerializeField] private Button _buyUpgradeButton;
    [Space(10), SerializeField] private double _startUpgradePrice;
    [SerializeField] private int _maxUpgradeLevel;
    [Space(10), SerializeField] private double[] _priceMultipliers;

    private int _upgradeLevel = 1;
    private double _upgradePrice;
    private Wallet _wallet;

    public void RemoveBuyButtonListeners() =>
        _buyUpgradeButton.onClick.RemoveAllListeners();

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _buyUpgradeButton.onClick.AddListener(() => TryBuyUpgrade());

        if (_upgradeLevel == 1)
            _upgradePrice = _startUpgradePrice;

        InvokeLevelChangeEvent(_upgradePrice, _upgradeLevel);
    }

    private void TryBuyUpgrade()
    {
        if (CanBuyUpgrade)
        {
            _wallet.TryReduceMoney(_upgradePrice);
            _upgradePrice *= _priceMultipliers[_upgradeLevel - 1];
            _upgradeLevel++;

            InvokeBuyEvent();
            InvokeLevelChangeEvent(_upgradePrice, _upgradeLevel);
        }
    }

    protected abstract void InvokeBuyEvent();
    protected abstract void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel);
}
