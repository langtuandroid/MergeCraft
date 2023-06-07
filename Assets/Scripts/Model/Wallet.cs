using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public event UnityAction<float> MoneyCountChanged;

    private float _money;
    private int _buildBlocks;

    public void TryAddReward(float moneyCount, int buildBlockCount)
    {
        if (moneyCount > 0 && buildBlockCount > 0)
        {
            _money += moneyCount;
            _buildBlocks += buildBlockCount;

            MoneyCountChanged?.Invoke(_money);
        }
    }
}
