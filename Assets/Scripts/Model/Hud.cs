using UnityEngine;
using UnityEngine.Events;

public class Hud : MonoBehaviour
{
    public event UnityAction HudInitialized;

    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private MoneyGenerator _moneyGenerator;
    [SerializeField] private WalletPresenter _walletPresenter;

    public void Initialize(Wallet wallet)
    {
        _blockCreator.Initialize(wallet);
        _moneyGenerator.Initialize(wallet);
        _walletPresenter.Initialize(wallet);

        HudInitialized?.Invoke();
    }
}
