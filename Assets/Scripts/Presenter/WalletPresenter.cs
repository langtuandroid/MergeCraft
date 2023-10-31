using UnityEngine;
using YG;

public class WalletPresenter : MonoBehaviour
{
    [SerializeField] private WalletContentShower _walletContentShower;
    [SerializeField] private SoundPlayer _soundPlayer;

    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.MoneyCountChanged += OnMoneyCountChanged;
        _wallet.BuildBlocksMoneyChanged += OnBuildBlocksMoneyChanged;
    }

    private void OnMoneyCountChanged(double money)
    {
        YandexGame.savesData.Money = money;
        YandexGame.SaveProgress();

        _walletContentShower.ShowMoneyCount(money);
        _soundPlayer.PlayMoneySound();
    }

    private void OnBuildBlocksMoneyChanged(int buildBlocksMoney)
    {
        _walletContentShower.ShowBuildBlockMoneyCount(buildBlocksMoney);
        YandexGame.savesData.BuildBlocksMoney = buildBlocksMoney;
        YandexGame.SaveProgress();
    }


    private void OnDisable()
    {
        _wallet.MoneyCountChanged -= OnMoneyCountChanged;
        _wallet.BuildBlocksMoneyChanged -= OnBuildBlocksMoneyChanged;
    }
}
