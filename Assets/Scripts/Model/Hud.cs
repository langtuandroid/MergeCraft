using UnityEngine;
using UnityEngine.Events;

public class Hud : MonoBehaviour
{
    public event UnityAction HudInitialized;

    [SerializeField] private BlockCreator _blockCreator;
    [SerializeField] private BlockReplacer _blockReplacer;
    [SerializeField] private ShopPresenter _shopPresenter;
    [SerializeField] private MoneyGenerator _moneyGenerator;
    [SerializeField] private WalletPresenter _walletPresenter;
    [SerializeField] private BuildingCreator _buildingCreator;

    public void Initialize(Wallet wallet)
    {
        _shopPresenter.Initialize(wallet);
        _blockCreator.Initialize(wallet);
        _blockReplacer.Initialize(wallet);
        _moneyGenerator.Initialize(wallet);
        _walletPresenter.Initialize(wallet);
        _buildingCreator.Initialize(wallet);

        HudInitialized?.Invoke();
    }
}
