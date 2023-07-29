using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockCreator : MonoBehaviour, IActivatable
{
    public event UnityAction<float> CreationProgressChanged;
    public bool CreatorActivated => _creatorActivated;

    [SerializeField] private float _creationDuration;
    [SerializeField] private int _creationBlockLevel;
    [Space(10), SerializeField] private Cell[] _cells;
    [SerializeField] private MergeBlock[] _blocks;

    private List<Cell> _emptyCells = new List<Cell>();
    private Wallet _wallet;
    private float _passedTime;
    private bool _canCreate;
    private bool _creatorActivated;

    public void Initialize(Wallet wallet) => _wallet = wallet;
    public void Activate() => _creatorActivated = true;

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
    }

    private void InitializeEmptyCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].Blocked == false && _cells[i].BlockInCell == null && _emptyCells.Contains(_cells[i]) == false)
                _emptyCells.Add(_cells[i]);
        }
    }

    private void TryResetPassedTime()
    {
        if (_passedTime >= _creationDuration)
        {
            _canCreate = true;
            _passedTime = 0;
        }
    }

    private void Awake() => _passedTime = _creationDuration;
}

