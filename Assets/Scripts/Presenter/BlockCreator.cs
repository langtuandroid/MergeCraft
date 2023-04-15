using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockCreator : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private MergeBlock[] _blocks;
    [SerializeField] private Slider _creationProgressSlider;
    [Space(10), SerializeField] private float _creationDuration;
    [SerializeField] private int _creationBlockLevel;

    private List<Cell> _emptyCells = new List<Cell>();
    private float _passedTime;
    private bool _canCreate;

    private void InitializeEmptyCells()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].Blocked == false && _cells[i].Occupied == false && _emptyCells.Contains(_cells[i]) == false)
                _emptyCells.Add(_cells[i]);
        }
    }
    
    private void TryCreateBlock()
    {
        if (_emptyCells.Count > 0)
        {
            if (_canCreate == true)
            {
                _canCreate = false;
                int emptyCellNumber = Random.Range(0, _emptyCells.Count);

                MergeBlock block = Instantiate(_blocks[_creationBlockLevel], _emptyCells[emptyCellNumber].transform.position, Quaternion.identity);
                _emptyCells[emptyCellNumber].Occupie(block.transform);
                _emptyCells.Clear();

                MergeBlockAnimator mergeBlockAnimator = block.GetComponent<MergeBlockAnimator>();
                mergeBlockAnimator.LaunchCreateBlockAnimation(_creationDuration);
            }

            _passedTime += Time.deltaTime;
            _creationProgressSlider.value = _passedTime;

            if (_passedTime >= _creationDuration)
            {
                _canCreate = true;
                _passedTime = 0;
            }
        }
    }

    private void Update()
    {
        InitializeEmptyCells();
        TryCreateBlock();
    }

    private void Awake()
    {
        _creationProgressSlider.maxValue = _creationDuration;
        _creationProgressSlider.minValue = 0;
        _passedTime = _creationDuration;
    }
}

