using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class BlockCreator : MonoBehaviour
{
    public event UnityAction<float> CreationProgressChanged;
    public bool CreatorActivated => _creatorActivated;
    public int CreationBlockLevel => _creationBlockLevel;

    [SerializeField] private Cell[] _cells;
    [SerializeField] private MergeBlock[] _blocks;

    private List<Cell> _emptyCells = new List<Cell>();
    private List<int> _blocksInCells = new List<int>();
    private Wallet _wallet;
    private float _passedTime;
    private bool _canCreate;
    private bool _creatorActivated;
    private int _creationBlockLevel = 0;
    private float _creationDuration = 3;
    private float _durationDecreaseStep = 0.1f;

    public void Initialize(Wallet wallet) => _wallet = wallet;

    public void TryDecreaseCreationDuration()
    {
        if (_creationDuration - _durationDecreaseStep >= 0)
        {
            _creationDuration -= _durationDecreaseStep;

            YandexGame.savesData.CreationDuration = _creationDuration;
            YandexGame.SaveProgress();
        }
    }

    public void TryIncreaseBlockLevel()
    {
        if (_creationBlockLevel + 1 < _blocks.Length)
        {
            _creationBlockLevel++;

            YandexGame.savesData.CreationBlockLevel = _creationBlockLevel;
            YandexGame.SaveProgress();
        }
    }

    public void TryRecoverBlocks()
    {
        SavesYG savesData = YandexGame.savesData;

        _creationBlockLevel = savesData.CreationBlockLevel;
        _creationDuration = savesData.CreationDuration;

        if (savesData.BlocksInCells != null && savesData.BlocksInCells.Count > 0)
        {
            for (int i = 0; i < savesData.BlocksInCells.Count; i++)
            {
                MergeBlock blockInCell = Instantiate(_blocks[savesData.BlocksInCells[i]], _cells[i].transform.position, Quaternion.identity);
                _cells[i].Occupie(blockInCell);

                blockInCell.RewardChest.Initialize(_wallet);
                blockInCell.MergeBlockAnimator.LaunchCreateBlockAnimation(_creationDuration);
            }

            _creatorActivated = true;
        }
        else
        {
            CreateAllBlocks();
            _creatorActivated = true;
        }
    }

    public void TryCreateBlock()
    {
        InitializeEmptyCells();

        if (_emptyCells.Count > 0)
        {
            _emptyCells.Clear();
            InitializeEmptyCells();

            if (_canCreate == true)
                CreateBlock();

            _passedTime += Time.deltaTime;
            CreationProgressChanged?.Invoke(_passedTime / _creationDuration);

            TryResetPassedTime();
        }
    }

    private void CreateBlock()
    {
        _canCreate = false;
        int emptyCellNumber = Random.Range(0, _emptyCells.Count);

        MergeBlock block = Instantiate(_blocks[_creationBlockLevel], _emptyCells[emptyCellNumber].transform.position, Quaternion.identity);
        _emptyCells[emptyCellNumber].Occupie(block);
        _emptyCells.Clear();

        block.RewardChest.Initialize(_wallet);
        block.MergeBlockAnimator.LaunchCreateBlockAnimation(_creationDuration);
        InitializeBlocksInCells();
    }

    private void InitializeEmptyCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].BlockInCell == null && _emptyCells.Contains(_cells[i]) == false)
                _emptyCells.Add(_cells[i]);
        }
    }

    private void InitializeBlocksInCells()
    {
        _blocksInCells.Clear();

        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].BlockInCell != null)
                _blocksInCells.Add(_cells[i].BlockInCell.BlockLevel - 1);
        }

        YandexGame.savesData.BlocksInCells = _blocksInCells;
        YandexGame.SaveProgress();
    }

    private void TryResetPassedTime()
    {
        if (_passedTime >= _creationDuration)
        {
            _canCreate = true;
            _passedTime = 0;
        }
    }

    private void CreateAllBlocks()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            _emptyCells.Clear();
            InitializeEmptyCells();
            CreateBlock();
        }
    }

    private void Awake() => _passedTime = _creationDuration;
}

