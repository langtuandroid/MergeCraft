using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    public int BlocksCount => _blocks.Count;
    public int BuildBlockPrice => _buildBlockPrice;
    public int BuildedBlocksCount => _buildedBlocks.Count;
    public bool CanBuyBlock => _wallet.BuildBlocksMoney >= _buildBlockPrice;
    public bool BlocksEnough => _buildedBlocks.Count < _blocks.Count && _remainingBlocks.Count > 0;

    public event UnityAction<BuildBlock> BlockActivated;
    public event UnityAction<int, int> BlocksCountChanged;

    [SerializeField] private int _buildBlockPrice;
    [Space(10), SerializeField] private List<BuildBlock> _blocks;

    private List<BuildBlock> _buildedBlocks = new List<BuildBlock>();
    private List<BuildBlock> _remainingBlocks = new List<BuildBlock>();
    private Wallet _wallet;
    private int _reward;

    public void ApplyBuildingReward() => _wallet.TryAddMoney(_reward);

    public void Intialize(Wallet wallet, int reward)
    {
        _wallet = wallet;
        _reward = reward;
    }

    public void TryBuildBlock()
    {
        if (BlocksEnough && CanBuyBlock)
        {
            _wallet.TryReduceBuildBlocksMoney(_buildBlockPrice);
            int blockNumber = Random.Range(0, _remainingBlocks.Count);

            _remainingBlocks[blockNumber].gameObject.SetActive(true);
            BlockActivated?.Invoke(_remainingBlocks[blockNumber]);

            _buildedBlocks.Add(_remainingBlocks[blockNumber]);
            _remainingBlocks.RemoveAt(blockNumber);
            BlocksCountChanged?.Invoke(_buildedBlocks.Count, _blocks.Count);
        }
    }

    public void CalculateBlocksCount()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].gameObject.activeInHierarchy == true)
                _buildedBlocks.Add(_blocks[i]);
            else
                _remainingBlocks.Add(_blocks[i]);
        }

        BlocksCountChanged?.Invoke(_buildedBlocks.Count, _blocks.Count);
    }
}
