using UnityEngine.Events;
using YG;

public class Wallet
{
    public double Money => _money;
    public int BuildBlocksMoney => _buildBlocksMoney;
    public float MoneyMultiplier => _moneyMultiplier;
    public int AdditionalBlockMoney => _additionalBlockMoney;

    public event UnityAction<double> MoneyCountChanged;
    public event UnityAction<int> BuildBlocksMoneyChanged;

    private double _money;
    private int _buildBlocksMoney;
    private float _moneyMultiplier = 1;
    private float _moneyMultiplierBeforeAd;
    private int _additionalBlockMoney = 0;
    private bool _adMoneyMultiplierActivated;
    private readonly float _moneyMultiplierIncreaseStep = 0.05f;
    private readonly int _blockMoneyIncreaseStep = 5;
    private const float _adMultiplierValue = 2;

    public void AddSavesDataListener() => YandexGame.GetDataEvent += OnGetDataEvent;

    public void RevertMoneyMultiplier()
    {
        _moneyMultiplier = _moneyMultiplierBeforeAd;
        _adMoneyMultiplierActivated = false;
    }

    public void ActivateAdMoneyMultiplier()
    {
        _moneyMultiplierBeforeAd = _moneyMultiplier;
        _moneyMultiplier = _adMultiplierValue;
        _adMoneyMultiplierActivated = true;
    }

    public void TryIncreaseMoneyMultiplier()
    {
        if (_moneyMultiplier + _moneyMultiplierIncreaseStep <= float.MaxValue && _adMoneyMultiplierActivated == false)
        {
            _moneyMultiplier += _moneyMultiplierIncreaseStep;

            YandexGame.savesData.MoneyMultiplier = _moneyMultiplier;
            YandexGame.SaveProgress();
        }
        else if (_moneyMultiplierBeforeAd + _moneyMultiplierIncreaseStep <= float.MaxValue && _adMoneyMultiplierActivated == true)
        {
            _moneyMultiplierBeforeAd += _moneyMultiplierIncreaseStep;
        }
    }

    public void TryIncreaseAdditionalBlockMoney()
    {
        if (_additionalBlockMoney + _blockMoneyIncreaseStep <= int.MaxValue)
        {
            _additionalBlockMoney += _blockMoneyIncreaseStep;

            YandexGame.savesData.AdditionalBlockMoney = _additionalBlockMoney;
            YandexGame.SaveProgress();
        }
    }

    public void TryAddMoney(double money)
    {
        if (money > 0)
        {
            _money += money * _moneyMultiplier;
            MoneyCountChanged?.Invoke(_money);
        }
    }

    public void TryReduceMoney(double money)
    {
        if (money > 0 && money <= _money)
        {
            _money -= money;
            MoneyCountChanged?.Invoke(_money);
        }
    }

    public void TryAddBuildBlocksMoney(int buildBlockMoney)
    {
        if (buildBlockMoney > 0)
        {
            _buildBlocksMoney += buildBlockMoney + _additionalBlockMoney;
            BuildBlocksMoneyChanged?.Invoke(_buildBlocksMoney);
        }
    }

    public void TryReduceBuildBlocksMoney(int buildBlockMoney)
    {
        if (buildBlockMoney > 0 && buildBlockMoney <= _buildBlocksMoney)
        {
            _buildBlocksMoney -= buildBlockMoney;
            BuildBlocksMoneyChanged?.Invoke(_buildBlocksMoney);
        }
    }

    private void OnGetDataEvent()
    {
        SavesYG savesData = YandexGame.savesData;

        _money = savesData.Money;
        _moneyMultiplier = savesData.MoneyMultiplier;
        _buildBlocksMoney = savesData.BuildBlocksMoney;
        _additionalBlockMoney = savesData.AdditionalBlockMoney;

        MoneyCountChanged?.Invoke(_money);
        BuildBlocksMoneyChanged?.Invoke(_buildBlocksMoney);

        YandexGame.GetDataEvent -= OnGetDataEvent;
    }
}
