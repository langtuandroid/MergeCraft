using UnityEngine;

public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private WalletContentShower _walletContentShower;

    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.MoneyCountChanged += OnMoneyCountChanged;
        _wallet.BuildBlocksMoneyChanged += OnBuildBlocksMoneyChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyCountChanged -= OnMoneyCountChanged;
        _wallet.BuildBlocksMoneyChanged -= OnBuildBlocksMoneyChanged;
    }

    private void OnMoneyCountChanged(double money) => _walletContentShower.ShowMoneyCount(money);
    private void OnBuildBlocksMoneyChanged(int buildBlocksMoney) => _walletContentShower.ShowBuildBlockMoneyCount(buildBlocksMoney);
}
