using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class Building : MonoBehaviour
{
    public int BlocksCount => _blocks.Count;
    public int BuildBlockPrice => _buildBlockPrice;
    public int BuildedBlocksCount => _buildedBlocks.Count;
    public bool CanBuyBlock => _wallet.BuildBlocksMoney >= _buildBlockPrice;
    public bool BlocksEnough => _buildedBlocks.Count < _blocks.Count && _remainingBlocks.Count > 0;

    public event UnityAction<BuildBlock> BlockActivated;
    public event UnityAction<int, int> BlocksCountChanged;

    [SerializeField] private List<BuildBlock> _blocks;

    private List<BuildBlock> _buildedBlocks = new List<BuildBlock>();
    private List<BuildBlock> _remainingBlocks = new List<BuildBlock>();
    private List<bool> _buildingBlocksActivity = new List<bool>();
    private Wallet _wallet;
    private double _reward;
    private int _buildBlockPrice;

    public void ApplyBuildingReward() => _wallet.TryAddMoney(_reward);

    public void Intialize(Wallet wallet, double reward, int buildBlockPrice)
    {
        _wallet = wallet;
        _reward = reward;
        _buildBlockPrice = buildBlockPrice;
    }

    public void TryBuildBlock()
    {
        if (CanBuyBlock && BlocksEnough)
        {
            _wallet.TryReduceBuildBlocksMoney(_buildBlockPrice);
            BuildBlock();
        }
    }

    public void BuildAllBlocks()
    {
        while (BlocksEnough)
            BuildBlock();
    }

    public void TryRestoreBuildedBlocks()
    {
        SavesYG savesData = YandexGame.savesData;

        if (savesData.BuildingBlocksActivity != null && savesData.BuildingBlocksActivity.Count > 0)
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (savesData.BuildingBlocksActivity[i] == true)
                    _blocks[i].gameObject.SetActive(true);
            }
        }

        CalculateBlocksCount();
    }

    private void CalculateBlocksCount()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].gameObject.activeSelf == true)
                _buildedBlocks.Add(_blocks[i]);
            else
                _remainingBlocks.Add(_blocks[i]);
        }

        BlocksCountChanged?.Invoke(_buildedBlocks.Count, _blocks.Count);
        SaveBuildedBlocksActivity();
    }

    private void BuildBlock()
    {
        int blockNumber = Random.Range(0, _remainingBlocks.Count);

        _remainingBlocks[blockNumber].gameObject.SetActive(true);
        BlockActivated?.Invoke(_remainingBlocks[blockNumber]);

        _buildedBlocks.Add(_remainingBlocks[blockNumber]);
        _remainingBlocks.RemoveAt(blockNumber);
        BlocksCountChanged?.Invoke(_buildedBlocks.Count, _blocks.Count);
        SaveBuildedBlocksActivity();
    }

    private void SaveBuildedBlocksActivity()
    {
        _buildingBlocksActivity.Clear();

        for (int i = 0; i < _blocks.Count; i++)
            _buildingBlocksActivity.Add(_blocks[i].gameObject.activeSelf);

        YandexGame.savesData.BuildingBlocksActivity = _buildingBlocksActivity;
        YandexGame.SaveProgress();
    }
}
