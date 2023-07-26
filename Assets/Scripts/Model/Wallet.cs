using UnityEngine.Events;

public class Wallet
{
    public event UnityAction<double> MoneyCountChanged;

    private double _money;
    private int _buildBlocks;

    public void TryAddMoney(double moneyCount)
    {
        if (moneyCount > 0)
        {
            _money += moneyCount;
            MoneyCountChanged?.Invoke(_money);
        }
    }

    public void TryAddBuildBlocks(int buildBlockCount)
    {
        if (buildBlockCount > 0)
            _buildBlocks += buildBlockCount;
    }
}
