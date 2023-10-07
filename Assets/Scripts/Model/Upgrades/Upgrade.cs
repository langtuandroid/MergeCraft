using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade
{
    public Button BuyUpgradeButton => _buyUpgradeButton;
    public bool CanBuyUpgrade => _wallet.Money >= _upgradePrice && _upgradeLevel < _maxUpgradeLevel;

    [SerializeField] private Button _buyUpgradeButton;
    [SerializeField] private Image _finishedUpgradeImage;
    [Space(10), SerializeField] private int _maxUpgradeLevel;
    [Space(10), SerializeField] private double[] _prices;

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
            _upgradePrice = _prices[0];

        InvokeLevelChangeEvent(_upgradePrice, _upgradeLevel);
    }

    private void TryBuyUpgrade()
    {
        if (CanBuyUpgrade)
        {
            _wallet.TryReduceMoney(_upgradePrice);
            _upgradePrice = _prices[_upgradeLevel];
            _upgradeLevel++;

            InvokeBuyEvent();
            InvokeLevelChangeEvent(_upgradePrice, _upgradeLevel);
            TryDeactivateUpgradeButton();
        }
    }

    private void TryDeactivateUpgradeButton()
    {
        if (_upgradeLevel == _maxUpgradeLevel)
        {
            _buyUpgradeButton.gameObject.SetActive(false);
            _finishedUpgradeImage.gameObject.SetActive(true);
        }    
    }

    protected abstract void InvokeBuyEvent();
    protected abstract void InvokeLevelChangeEvent(double upgradePrice, int upgradeLevel);
}
