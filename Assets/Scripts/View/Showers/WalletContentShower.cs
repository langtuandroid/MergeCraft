using TMPro;
using UnityEngine;

public class WalletContentShower : MonoBehaviour, ICurrencyShower
{
    [SerializeField] private TMP_Text _moneyShower;
    [SerializeField] private TMP_Text _buildBlockShower;

    public void ShowMoneyCount(double money) => _moneyShower.text = NumberFormater.FormatNumber(money);
    public void ShowBuildBlockMoneyCount(int buildBlocks) => _buildBlockShower.text = buildBlocks.ToString();
}
