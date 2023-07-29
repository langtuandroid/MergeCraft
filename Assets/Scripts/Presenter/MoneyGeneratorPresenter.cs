using UnityEngine;

public class MoneyGeneratorPresenter : MonoBehaviour
{
    [SerializeField] private MoneyGenerator _moneyGenerator;

    private void Update()
    {
        if (_moneyGenerator.GeneratorActivated == true)
            _moneyGenerator.TryLaunchGeneration();
    }
}
