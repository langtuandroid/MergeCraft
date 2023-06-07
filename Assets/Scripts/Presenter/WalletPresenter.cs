using UnityEngine;

public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private RewardShower _moneyShower;

    private void OnMoneyCountChanged(float money) => _moneyShower.Show(money);
    private void OnEnable() => _wallet.MoneyCountChanged += OnMoneyCountChanged;
    private void OnDisable() => _wallet.MoneyCountChanged -= OnMoneyCountChanged;
}
