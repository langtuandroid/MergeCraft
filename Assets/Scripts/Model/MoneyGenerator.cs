using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    [SerializeField] private AnimationCurve _blockLevelMoney;
    [SerializeField] private AnimationCurve _delayDecreasePrice;
    [SerializeField] private AnimationCurve _multiplierIncreasePrice;
    [Space(10), SerializeField] private float _moneyGenerationDelay;
    [SerializeField] private float _moneyMultiplier;
    [SerializeField] private Wallet _wallet;
    [Space(10), SerializeField] private Cell[] _cells;

    private float _generatedMoney;
    private float _passedTime;
    private readonly float _multiplierIncreaseStep = 0.1f;

    public void IncreaseMultiplier() => _moneyMultiplier += _multiplierIncreaseStep;

    public void GenerateMoney()
    {
        _generatedMoney = 0;

        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].BlockInCell != null)
            {
                if (_cells[i].BlockInCell.GetComponent<MergeBlockAnimator>().AnimationPlaying == false)
                {
                    float moneyForBlock = (_blockLevelMoney.Evaluate(_cells[i].BlockInCell.BlockLevel) * _moneyMultiplier);
                    _generatedMoney += moneyForBlock;

                    _cells[i].BlockInCell.gameObject.GetComponent<RewardShower>().Show(moneyForBlock);
                    _cells[i].BlockInCell.gameObject.GetComponent<RewardAnimator>().LaunchGettingRewardAnimation();
                }
            }
        }

        //_wallet.TryAddReward(_generatedMoney);
    }

#if UNITY_EDITOR
    public List<float> GetBlocksLevelMoney()
    {
        List<float> blocksLevelMoney = new List<float>();

        for (int i = 0; i < 48; i++)
            blocksLevelMoney.Add(_blockLevelMoney.Evaluate(i) * _moneyMultiplier);

        return blocksLevelMoney;
    }
#endif

    private void Update()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime >= _moneyGenerationDelay)
        {
            GenerateMoney();
            _passedTime = 0;
        }
    }
}
