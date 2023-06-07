using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] private Image _creationProgressImage;
    [SerializeField] private Wallet _wallet;
    [Space(10), SerializeField] private float _creationDuration;
    [SerializeField] private int _creationBlockLevel;
    [Space(10), SerializeField] private Cell[] _cells;
    [SerializeField] private MergeBlock[] _blocks;

    private List<Cell> _emptyCells = new List<Cell>();
    private float _passedTime;
    private bool _canCreate;

    private void InitializeEmptyCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].Blocked == false && _cells[i].BlockInCell == null && _emptyCells.Contains(_cells[i]) == false)
                _emptyCells.Add(_cells[i]);
        }
    }
    
    private void TryCreateBlock()
    {
        InitializeEmptyCells();

        if (_emptyCells.Count > 0)
        {
            _emptyCells.Clear();
            InitializeEmptyCells();

            if (_canCreate == true)
            {
                _canCreate = false;
                int emptyCellNumber = Random.Range(0, _emptyCells.Count);

                MergeBlock block = Instantiate(_blocks[_creationBlockLevel], _emptyCells[emptyCellNumber].transform.position, Quaternion.identity);
                _emptyCells[emptyCellNumber].Occupie(block);
                _emptyCells.Clear();

                block.GetComponent<RewardChest>().Initialize(_wallet);
                block.GetComponent<MergeBlockAnimator>().LaunchCreateBlockAnimation(_creationDuration);
            }

            _passedTime += Time.deltaTime;
            _creationProgressImage.fillAmount = _passedTime / _creationDuration;

            if (_passedTime >= _creationDuration)
            {
                _canCreate = true;
                _passedTime = 0;
            }
        }
    }

    private void Update() => TryCreateBlock();
    private void Awake() => _passedTime = _creationDuration;
}

