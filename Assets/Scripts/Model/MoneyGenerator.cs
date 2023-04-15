using UnityEngine;
using UnityEngine.Events;

public class MoneyGenerator : MonoBehaviour
{
    public event UnityAction<int> MoneyGenerated;

    [SerializeField] private Wallet _wallet;
    [SerializeField] private float _moneyStep;
    [SerializeField] private float _moneyMultiplier;

    public void GenerateMoney()
    {
        int generatedMoney = Mathf.RoundToInt(_moneyStep * _moneyMultiplier);

        _wallet.TryAddMoney(generatedMoney);
        MoneyGenerated?.Invoke(generatedMoney);
    }
}
