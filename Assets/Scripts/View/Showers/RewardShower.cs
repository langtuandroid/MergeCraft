using UnityEngine;
using TMPro;

public class RewardShower : MonoBehaviour, ICurrencyShower
{
    [SerializeField] private TMP_Text _moneyRewardShower;
    [SerializeField] private TMP_Text _blockRewardShower;

    public void ShowMoneyCount(double money) => _moneyRewardShower.text = NumberFormater.FormatNumber(money);
    public void ShowBuildBlockCount(int buildBlocks) => _blockRewardShower.text = buildBlocks.ToString();
}
