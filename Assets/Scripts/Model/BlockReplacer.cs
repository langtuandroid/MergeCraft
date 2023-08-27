using UnityEngine;

public class BlockReplacer : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private MergeBlock[] _blocks;

    private Wallet _wallet;

    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryReplaceBlocks(int creationBlockLevel)
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].BlockInCell != null)
            {
                if (_cells[i].BlockInCell.BlockLevel < creationBlockLevel + 1)
                {
                    Destroy(_cells[i].BlockInCell.gameObject);

                    MergeBlock mergedBlock = Instantiate(_blocks[creationBlockLevel], _cells[i].transform.position, Quaternion.identity);
                    _cells[i].Occupie(mergedBlock);

                    mergedBlock.RewardChest.Initialize(_wallet);
                    mergedBlock.MergeBlockAnimator.LaunchMergeBlockAnimation();
                }
            }
        }
    }
}
