using UnityEngine.Events;

public class Wallet
{
    public double Money => _money;
    public int BuildBlocksMoney => _buildBlocksMoney;

    public event UnityAction<double> MoneyCountChanged;
    public event UnityAction<int> BuildBlocksMoneyChanged;

    private double _money;
    private int _buildBlocksMoney;

    public void TryIncreaseMoney(double money)
    {
        if (money > 0)
        {
            _money += money;
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
            _buildBlocksMoney += buildBlockMoney;
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
