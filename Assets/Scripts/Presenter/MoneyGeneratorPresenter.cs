using UnityEngine;

public class MoneyGeneratorPresenter : MonoBehaviour
{
    [SerializeField] private MoneyGenerator _moneyGenerator;

    private void OnMoneyGenerated(int money)
    {

    }

    private void OnEnable() => _moneyGenerator.MoneyGenerated += OnMoneyGenerated;
    private void OnDisable() => _moneyGenerator.MoneyGenerated -= OnMoneyGenerated;
}
