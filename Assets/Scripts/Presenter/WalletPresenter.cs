using UnityEngine;

public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private WalletContentShower _walletContentShower;

    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.MoneyCountChanged += OnMoneyCountChanged;
    }

    private void OnMoneyCountChanged(double money) => _walletContentShower.ShowMoneyCount(money);
    private void OnDisable() => _wallet.MoneyCountChanged -= OnMoneyCountChanged;
}
