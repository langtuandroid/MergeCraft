using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    public event UnityAction BlockBuilded;
    public int BlocksCount => _blocks.Count;
    public int BuildedBlocksCount => _buildedBlocks.Count;

    [SerializeField] private List<GameObject> _blocks;

    private List<GameObject> _buildedBlocks = new List<GameObject>();
    private List<GameObject> _remainingBlocks = new List<GameObject>();
 
    private bool CanBuildBlock => _buildedBlocks.Count < _blocks.Count && _remainingBlocks.Count > 0;

    public void TryBuildBlock()
    {
        if (CanBuildBlock)
        {
            int blockNumber = Random.Range(0, _remainingBlocks.Count);

            _remainingBlocks[blockNumber].SetActive(true);
            _buildedBlocks.Add(_remainingBlocks[blockNumber]);
            _remainingBlocks.RemoveAt(blockNumber);

            BlockBuilded?.Invoke();
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            if (_blocks[i].activeInHierarchy == true)
                _buildedBlocks.Add(_blocks[i]);
            else
                _remainingBlocks.Add(_blocks[i]);
        }
    }

    private void Start() => Initialize();
}
