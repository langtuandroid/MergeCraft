using UnityEngine;
using UnityEngine.UI;

public class ShopPresenter : MonoBehaviour
{
    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private Notifications _notifications;
    [SerializeField] private MoneyGenerator _moneyGenerator;
    [SerializeField] private UpgradesPricesShower _upgradesPricesShower;
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Button _openShopButton;
    [SerializeField] private GameObject _shopPanel;
    [Space(10), SerializeField] private BlockLevelUpgrade[] _upgrades;
    [Space(10), SerializeField] private BlockLevelUpgrade _blockLevelUpgrade;

    private Wallet _wallet;

    public void Initialize(Wallet wallet) => _wallet = wallet;

    private void Update()
    {
        int enoughCount = 0;

        for (int i = 0; i < _upgrades.Length; i++)
        {
            if (_upgrades[i].CanBuyUpgrade)
            {
                _upgrades[i].BuyUpgradeButton.interactable = true;
                _notifications.ActivateShopNotification();
                enoughCount = 0;
            }
            else
            {
                _upgrades[i].BuyUpgradeButton.interactable = false;
                enoughCount++;

                if (enoughCount == _upgrades.Length)
                    _notifications.DeactivateShopNotification();
            }
        }
    }

    private void OnEnable()
    {
        _openShopButton.onClick.AddListener(() => _shopPanel.SetActive(true));
        _closeShopButton.onClick.AddListener(() => _shopPanel.SetActive(false));

        foreach (var upgrade in _upgrades)
        {
            upgrade.Initialize(_wallet);
        }
    }

    private void OnDisable()
    {
        _openShopButton.onClick.RemoveAllListeners();
        _closeShopButton.onClick.RemoveAllListeners();

        foreach (var upgrade in _upgrades)
        {
            upgrade.RemoveBuyButtonListeners();
        }
    }
}
