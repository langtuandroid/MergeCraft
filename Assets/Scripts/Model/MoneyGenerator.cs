using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour, IActivatable
{
    public bool GeneratorActivated => _generatorActivated;

    [SerializeField] private int _raiseValue;
    [Space(10), SerializeField] private float _moneyGenerationDelay;
    [SerializeField] private float _moneyMultiplier;
    [Space(10), SerializeField] private Cell[] _cells;

    private Wallet _wallet;
    private float _passedTime;
    private double _generatedMoney;
    private bool _generatorActivated;
    private readonly float _multiplierIncreaseStep = 0.05f;

    public void Activate() => _generatorActivated = true;
    public void Initialize(Wallet wallet) => _wallet = wallet;
    public void IncreaseMultiplier() => _moneyMultiplier += _multiplierIncreaseStep;

    public void TryLaunchGeneration()
    {
        _passedTime += Time.deltaTime;

        if (_passedTime >= _moneyGenerationDelay)
        {
            TryGenerateMoney();
            _passedTime = 0;
        }
    }

    private void TryGenerateMoney()
    {
        _generatedMoney = 0;

        for (int i = 0; i < _cells.Length; i++)
        {
            if (_cells[i].BlockInCell != null)
            {
                if (_cells[i].BlockInCell.MergeBlockAnimator.AnimationPlaying == false)
                {
                    double moneyForBlock = (Mathf.Pow(_raiseValue, _cells[i].BlockInCell.BlockLevel));
                    _generatedMoney += moneyForBlock;

                    _cells[i].BlockInCell.RewardShower.ShowMoneyCount(moneyForBlock);
                    _cells[i].BlockInCell.RewardAnimator.LaunchGettingMoneyRewardAnimation();
                }
            }
        }

        _wallet.TryAddMoney(_generatedMoney);
    }

#if UNITY_EDITOR
    public List<double> GetBlocksLevelMoney()
    {
        List<double> blocksLevelMoney = new List<double>();

        for (int i = 0; i < 49; i++)
            blocksLevelMoney.Add(Mathf.Pow(2, i));

        return blocksLevelMoney;
    }
#endif
}
