using UnityEngine;

public class RewardChest : MonoBehaviour
{
    public Wallet Wallet => _wallet;
    public int MoneyReward => _moneyReward;
    public int BuildBlockReward => _buildBlockReward;

    [SerializeField] private int _moneyReward;
    [SerializeField] private int _buildBlockReward;

    private Wallet _wallet;

    public void Initialize(Wallet wallet) => _wallet = wallet;
    public void Open() => _wallet.TryAddReward(_moneyReward, _buildBlockReward);
}


