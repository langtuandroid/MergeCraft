using UnityEngine;

public class RewardChest : MonoBehaviour
{
    public Wallet Wallet => _wallet;
    public int BuildBlockReward => _buildBlockReward * _wallet.BlockMoneyMultiplier;
    public bool RewardApplied => _rewardApplied;

    private readonly int _buildBlockReward = 5;

    private Wallet _wallet;
    private bool _rewardApplied;

    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryApplyReward()
    {
        if (_rewardApplied == false)
        {
            _wallet.TryIncreaseBuildBlocksMoney(_buildBlockReward);
            _rewardApplied = true;
        }
    }
}


