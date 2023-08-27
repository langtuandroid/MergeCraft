using UnityEngine.Events;

public class Wallet
{
    public double Money => _money;
    public int BuildBlocksMoney => _buildBlocksMoney;
    public float MoneyMultiplier => _moneyMultiplier;
    public int BlockMoneyMultiplier => _blockMoneyMultiplier;

    public event UnityAction<double> MoneyCountChanged;
    public event UnityAction<int> BuildBlocksMoneyChanged;

    private double _money;
    private int _buildBlocksMoney;
    private float _moneyMultiplier = 1;
    private int _blockMoneyMultiplier = 1;
    private readonly float _moneyMultiplierIncreaseStep = 0.05f;
    private readonly int _blockMoneyMultiplierIncreaseStep = 2;

    public void TryIncreaseMoneyMultiplier()
    {
        if (_moneyMultiplier + _moneyMultiplierIncreaseStep <= float.MaxValue)
            _moneyMultiplier += _moneyMultiplierIncreaseStep;
    }

    public void TryIncreaseBlockMoneyMultiplier()
    {
        if (_blockMoneyMultiplier + _blockMoneyMultiplierIncreaseStep <= int.MaxValue)
            _blockMoneyMultiplier *= _blockMoneyMultiplierIncreaseStep;
    }

    public void TryIncreaseMoney(double money)
    {
        if (money > 0)
        {
            _money += money * _moneyMultiplier;
            MoneyCountChanged?.Invoke(_money);
        }
    }

    public void TryDecreaseMoney(double money)
    {
        if (money > 0 && money <= _money)
        {
            _money -= money;
            MoneyCountChanged?.Invoke(_money);
        }
    }

    public void TryIncreaseBuildBlocksMoney(int buildBlockMoney)
    {
        if (buildBlockMoney > 0)
        {
            _buildBlocksMoney += buildBlockMoney * _blockMoneyMultiplier;
            BuildBlocksMoneyChanged?.Invoke(_buildBlocksMoney);
        }
    }

    public void TryDecreaseBuildBlocksMoney(int buildBlockMoney)
    {
        if (buildBlockMoney > 0 && buildBlockMoney <= _buildBlocksMoney)
        {
            _buildBlocksMoney -= buildBlockMoney;
            BuildBlocksMoneyChanged?.Invoke(_buildBlocksMoney);
        }
    }
}
